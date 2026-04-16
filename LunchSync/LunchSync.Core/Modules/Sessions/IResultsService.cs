using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchSync.Core.Modules.Sessions;
public interface IResultsService
{
    Task<GetResultsDto>  GetResultsAsync(string pin, CancellationToken ct = default);
    Task<BoomResultDto>  BoomAsync(string pin, Guid hostId, CancellationToken ct = default);
    Task<PickResultDto>  PickAsync(string pin, Guid hostId, Guid restaurantId, CancellationToken ct = default);
}
