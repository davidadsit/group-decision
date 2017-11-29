using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GroupDecisionMaker
{
    public class VotingReport
    {
        readonly List<Tuple<string, int>> candidates = new List<Tuple<string, int>>();
        readonly StringBuilder textReport;

        public VotingReport(int totalBallots)
        {
            TotalBallots = totalBallots;
            textReport = new StringBuilder();
        }

        public string Winner { get; set; }
        public int TotalBallots { get; }

        public string GetTextReportText()
        {
            var candidateReportBuilder = new StringBuilder();
            candidateReportBuilder.AppendLine($"{TotalBallots} ballots were submitted");
            candidateReportBuilder.AppendLine($" - {candidates.Sum(x => x.Item2)} ballots were used for the final selection");
            var exhaustedBallots = TotalBallots - candidates.Sum(x => x.Item2);
            candidateReportBuilder.AppendLine($" - {exhaustedBallots} ({exhaustedBallots * 100 / TotalBallots}%) ballots were exhausted");
            var winnerVoteCount = candidates.Single(x => x.Item1 == Winner).Item2;
            foreach (var candidate in candidates.OrderByDescending(x => x.Item2).ThenBy(x => x.Item1))
            {
                var vote = candidate.Item2 == 1 ? "vote" : "votes";
                var fewer = candidate.Item1 == Winner ? "" : $" ({winnerVoteCount - candidate.Item2} fewer than {Winner})";
                candidateReportBuilder.AppendLine($"{candidate.Item1} had {candidate.Item2} {vote} ({candidate.Item2*100/TotalBallots}%) {fewer}");
            }
            var details = textReport.Length > 0 ? $"{Environment.NewLine}{textReport}" : "";
            return $"{Winner} wins!{Environment.NewLine}" + details + $"{Environment.NewLine}{candidateReportBuilder}";
        }

        public void AppendLine(string reportLine)
        {
            textReport.AppendLine(reportLine);
        }

        public void AppendCandidate(string candidate, int votes)
        {
            candidates.Add(new Tuple<string, int>(candidate, votes));
        }
    }
}