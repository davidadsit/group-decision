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

        public VotingReport(string winner)
        {
            Winner = winner;
            textReport = new StringBuilder($"{winner} wins!{Environment.NewLine}");
        }

        public string Winner { get; }

        public string GetTextReportText()
        {
            var candidateReportBuilder = new StringBuilder();
            foreach (var candidate in candidates.OrderByDescending(x => x.Item2).ThenBy(x => x.Item1))
            {
                var vote = candidate.Item2 == 1 ? "vote" : "votes";
                candidateReportBuilder.AppendLine($"{candidate.Item1} had {candidate.Item2} {vote}");
            }
            return $"{textReport}{Environment.NewLine}{candidateReportBuilder}";
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