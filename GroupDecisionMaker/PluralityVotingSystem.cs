using System.Collections.Generic;
using System.Linq;

namespace GroupDecisionMaker
{
    public class PluralityVotingSystem
    {
        readonly List<Ballot> allBallots = new List<Ballot>();

        public string Winner
        {
            get
            {
                var counter = new Counter();
                var countingResult = counter.Count(allBallots.ToArray());
                if (countingResult.TopCandidates.Any() && countingResult.TopCandidates.Length > 1)
                {
                    return "Tie";
                }
                return countingResult.TopCandidates.FirstOrDefault() ?? "Inconclusive";
            }
        }

        public void RecordBallots(params Ballot[] ballots)
        {
            allBallots.AddRange(ballots);
        }
    }
}