using System.Collections.Generic;
using System.Linq;

namespace GroupDecisionMaker
{
    public class PluralityVotingSystem
    {
        readonly List<Ballot> allBallots = new List<Ballot>();
        
        public void RecordBallots(params Ballot[] ballots)
        {
            allBallots.AddRange(ballots);
        }

        public VotingReport BuildReport()
        {
            VotingReport votingReport;
            var counter = new Counter();
            var countingResult = counter.Count(allBallots.ToArray());

            if (countingResult.TopCandidates.Any() && countingResult.TopCandidates.Length > 1)
            {
                votingReport = new VotingReport("Tie");
            }
            else
            {
                votingReport = new VotingReport(countingResult.TopCandidates.FirstOrDefault() ?? "Inconclusive");
                foreach (var candidate in countingResult.AllCandidates)
                {   
                    votingReport.AppendCandidate(candidate, countingResult.Votes(candidate));
                }
            }

            return votingReport;
        }
    }
}