using LunchSync.Core.Common.Enums;
using LunchSync.Core.Common.Interfaces;
using LunchSync.Core.Exceptions;
using LunchSync.Core.Modules.Sessions;
using LunchSync.Core.Modules.VotingAndScoring.Config;
using LunchSync.Core.Modules.VotingAndScoring.Scoring;

namespace LunchSync.Core.Modules.VotingAndScoring;
public sealed class VotingService : IVotingService
{
    private readonly IUnitOfWork _uow;
    private readonly ScoringEngine _engine;
    private readonly SessionScoringService _scoringService;
    private readonly ISessionCache _sessionCache;

    public VotingService(
        IUnitOfWork uow,
        ScoringEngine engine,
        SessionScoringService scoringService, ISessionCache sessionCache)
    {
        _uow = uow;
        _engine = engine;
        _scoringService = scoringService;
        _sessionCache = sessionCache;
    }

    // ── GET choices ───────────────────────────────────────────────────────────

    public IReadOnlyList<BinaryChoiceDto> GetChoices() =>
        BinaryChoiceDefinitions.All
            .Select(bc => new BinaryChoiceDto(bc.Index, bc.Label, bc.OptionA, bc.OptionB))
            .ToList();

    // ── POST vote ─────────────────────────────────────────────────────────────

    public async Task<VoteResultDto> SubmitVoteAsync(
        string pin,
        Guid participantId,
        string choices,
        CancellationToken ct = default)
    {
        // Validate format first — fast fail before any DB call
        ScoringEngine.ValidateChoices(choices);

        // ── Load session (includes Participants) ──────────────────────────────
        var session = await _sessionCache.GetActiveSessionByPinAsync(pin, ct)
            ?? throw new SessionNotFoundException(pin);

        session.Participants = await _sessionCache.GetParticipantsAsync(pin, ct);

        if (session.Status != SessionStatus.Voting)
            throw new VoteNotReadyException("Session is not accepting votes right now.");

        var participant = session.Participants.FirstOrDefault(p => p.Id == participantId)
            ?? throw new ParticipantNotFoundException(participantId);

        if (participant.VotedAt is not null)
            throw new BusinessRuleViolationException("Participant has already voted.");

        // ── Build individual vector ───────────────────────────────────────────
        // Engine works in double[]; Participant.PrefVector is List<float>?
        double[] vectorDouble = _engine.BuildIndividualVector(choices);
        var vectorFloat = vectorDouble.Select(d => (float)d).ToList();

        // ── Persist participant changes via attach + targeted update ──────────
        // SessionRepository uses Attach + Property.IsModified pattern (AsNoTracking session).
        // We need a scoped context call — re-use the same pattern via SaveParticipantAsync
        // which does _context.Participants.Add. Instead we attach and mark modified.
        //
        // Because SessionRepository only exposes SaveParticipantAsync (INSERT) and
        // UpdateSessionAsync (targeted column update), we persist participant via
        // the UoW's DbContext directly through UpdateParticipantVoteAsync.
        participant.PrefVector = vectorFloat;
        participant.VotedAt = DateTime.UtcNow;

        await _uow.Sessions.UpdateParticipantVoteAsync(participant, ct);
        await _sessionCache.UpdateParticipantPrefVectorAsync(pin, participant, participant.PrefVector);

        session.Participants = await _sessionCache.GetParticipantsAsync(pin);
        // ── Check if all have voted ───────────────────────────────────────────
        // Re-read counts from the in-memory list; participant we just updated is
        // already mutated above, so the count is accurate.
        int totalParticipants = session.Participants.Count;
        int totalVoted = session.Participants.Count(p => p.VotedAt is not null);

        bool allVoted = totalVoted >= totalParticipants;

        if (allVoted)
            await _scoringService.RunAsync(pin, ct);

        return new VoteResultDto("Voted",totalParticipants, totalVoted, ScoringTriggered: allVoted);
    }

    // ── POST close-voting (host lazy evaluation) ──────────────────────────────

    public async Task CloseVotingAsync(string pin, Guid hostUserId, CancellationToken ct = default)
    {
        var session = await _sessionCache.GetActiveSessionByPinAsync(pin, ct)
            ?? throw new SessionNotFoundException(pin);

        if (session.HostId != hostUserId)
            throw new NotHostException();

        if (session.Status != SessionStatus.Voting)
            throw new VoteNotReadyException("Session is not in Voting state.");

        session.Participants = await _sessionCache.GetParticipantsAsync(pin);
        int voted = session.Participants.Count(p => p.VotedAt is not null);

        if (voted == 0)
            throw new VoteNotReadyException("No participants have voted yet.");

        await _scoringService.RunAsync(pin, ct);
    }
}
