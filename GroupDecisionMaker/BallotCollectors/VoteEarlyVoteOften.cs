using System.Collections.Generic;

namespace GroupDecisionMaker.BallotCollectors
{
    public class VoteEarlyVoteOften : IBallotCollector
    {
        readonly List<Ballot> collectedBallots = new List<Ballot>();
        public Ballot[] Ballots => collectedBallots.ToArray();

        public void RecordBallot(string voterId, Ballot ballot)
        {
            collectedBallots.Add(ballot);
        }
    }
}