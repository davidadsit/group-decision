namespace GroupDecisionMaker.BallotCollectors
{
    public interface IBallotCollector
    {
        void RecordBallot(string voterId, Ballot ballot);
        Ballot[] Ballots { get; }
    }
}