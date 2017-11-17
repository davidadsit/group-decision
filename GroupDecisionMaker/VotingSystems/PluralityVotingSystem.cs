using System;
using System.Linq;
using GroupDecisionMaker.BallotCollectors;

namespace GroupDecisionMaker.VotingSystems
{
    public class PluralityVotingSystem
    {
        readonly IBallotCollector ballotCollector;

        public PluralityVotingSystem(IBallotCollector ballotCollector)
        {
            this.ballotCollector = ballotCollector;
        }

        public void RecordBallots(params Ballot[] ballots)
        {
            foreach (var ballot in ballots)
            {
                ballotCollector.RecordBallot(Guid.NewGuid().ToString("N"), ballot);
            }
        }

        public VotingReport BuildReport()
        {
            VotingReport votingReport;
            var counter = new Counter();
            var countingResult = counter.Count(ballotCollector.Ballots);

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