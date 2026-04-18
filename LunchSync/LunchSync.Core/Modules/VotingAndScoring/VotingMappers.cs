using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LunchSync.Core.Modules.VotingAndScoring.Config;

namespace LunchSync.Core.Modules.VotingAndScoring;
public static class VotingMappers
{
    public static BinaryChoiceDto ToDto(this BinaryChoice bc) =>
        new(bc.Index, bc.Label, bc.OptionA, bc.OptionB);
}
