using System;
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

        public string BuildReport()
        {
            var counter = new Counter();
            var countingResult = counter.Count(allBallots.ToArray());

            string report;
            if (countingResult.TopCandidates.Any() && countingResult.TopCandidates.Length > 1)
            {
                report = "Tie";
            }
            else
            {
                report = $"{countingResult.TopCandidates.FirstOrDefault()} wins!{Environment.NewLine}{Environment.NewLine}";
                report = countingResult
                    .AllCandidates
                    .Aggregate(report, (current, candidate) => current + $"{candidate} had {countingResult.Votes(candidate)} votes{Environment.NewLine}");
            }

            return report.TrimEnd();
        }
    }
}