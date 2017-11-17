namespace GroupDecisionMaker.VotingSystems
{
    public interface IVotingSystem
    {
        void RecordBallots(params Ballot[] ballots);
        void RecordBallot(string voterId, Ballot ballot);
        VotingReport BuildReport();
    }
}