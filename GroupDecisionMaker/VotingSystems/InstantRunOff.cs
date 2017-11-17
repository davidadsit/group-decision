using System;
using System.Collections.Generic;
using System.Linq;
using GroupDecisionMaker.BallotCollectors;

namespace GroupDecisionMaker.VotingSystems
{
    public class InstantRunOff : IVotingSystem
    {
        readonly IBallotCollector ballotCollector;
        CountingResult countingResult;

        public InstantRunOff(IBallotCollector ballotCollector)
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
            var votingReport = new VotingReport();
            var exclusions = new List<string>();

            countingResult = counter.Count(ballotCollector.Ballots);
            var round = 1;
            while (NoMajorityExists() && CandidatesCanBeEliminated())
            {
                votingReport.AppendLine($"Inconclusive results after round {round}");
                foreach (var bottomCandidate in countingResult.BottomCandidates)
                {
                    var votes = countingResult.Votes(countingResult.BottomCandidates.First());
                    votingReport.AppendLine($" - {bottomCandidate} had {votes} votes and will be excluded from future rounds");
                }
                exclusions.AddRange(countingResult.BottomCandidates);
                countingResult = counter.CountWithExclusions(exclusions.ToArray(), ballotCollector.Ballots);
                round++;
            }

            if (countingResult.AllCandidates.Length == 0)
            {
                votingReport.Winner = "Inconclusive";
            }
            else if (!CandidatesCanBeEliminated())
            {
                votingReport.Winner = "Tie";
            }
            else
            {
                votingReport.Winner = countingResult.TopCandidates.First();
            }
            foreach (var candidate in countingResult.AllCandidates)
            {
                votingReport.AppendCandidate(candidate, countingResult.Votes(candidate));
            }

            return votingReport;
        }

        bool CandidatesCanBeEliminated()
        {
            return countingResult.AllCandidates.Length > 0 && countingResult.TopCandidates.Length != countingResult.AllCandidates.Length;
        }

        bool NoMajorityExists()
        {
            var top = countingResult.TopCandidates.FirstOrDefault();
            return countingResult.Votes(top) <= countingResult.TotalVotes / 2;
        }
    }
}