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
            candidateReportBuilder.AppendLine($" - {TotalBallots - candidates.Sum(x => x.Item2)} ballots were exhausted");
            foreach (var candidate in candidates.OrderByDescending(x => x.Item2).ThenBy(x => x.Item1))
            {
                var vote = candidate.Item2 == 1 ? "vote" : "votes";
                candidateReportBuilder.AppendLine($"{candidate.Item1} had {candidate.Item2} {vote}");
            }
            return $"{Winner} wins!{Environment.NewLine}{Environment.NewLine}{textReport}{Environment.NewLine}{Environment.NewLine}{candidateReportBuilder}";
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