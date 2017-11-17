using System.Collections.Generic;
using System.Linq;

namespace GroupDecisionMaker.BallotCollectors
{
    public class OnePersonOneVote : IBallotCollector
    {
        readonly Dictionary<string, Ballot> collectedBallots = new Dictionary<string, Ballot>();
        public Ballot[] Ballots => collectedBallots.Values.ToArray();

        public void RecordBallot(string voterId, Ballot ballot)
        {
            collectedBallots[voterId] = ballot;
        }
    }
}