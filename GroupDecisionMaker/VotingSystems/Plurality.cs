using System;
using System.Linq;
using GroupDecisionMaker.BallotCollectors;

namespace GroupDecisionMaker.VotingSystems
{
    public class Plurality : IVotingSystem
    {
        readonly IBallotCollector ballotCollector;

        public Plurality(IBallotCollector ballotCollector)
        {
            this.ballotCollector = ballotCollector;
        }

        public void RecordBallots(params Ballot[] ballots)
        {
            foreach (var ballot in ballots)
            {
                RecordBallot(Guid.NewGuid().ToString("N"), ballot);
            }
        }

        public void RecordBallot(string voterId, Ballot ballot)
        {
            ballotCollector.RecordBallot(voterId, ballot);
        }

        public VotingReport BuildReport()
        {
            var counter = new Counter();
            var countingResult = counter.Count(ballotCollector.Ballots);

            var votingReport = new VotingReport(ballotCollector.Ballots.Length);
            if (countingResult.TopCandidates.Any() && countingResult.TopCandidates.Length > 1)
            {
                votingReport.Winner = "Tie";
            }
            else
            {
                votingReport.Winner = countingResult.TopCandidates.FirstOrDefault() ?? "Inconclusive";
            }

            foreach (var candidate in countingResult.AllCandidates)
            {
                votingReport.AppendCandidate(candidate, countingResult.Votes(candidate));
            }

            return votingReport;
        }
    }
}