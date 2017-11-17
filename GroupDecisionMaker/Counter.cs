namespace GroupDecisionMaker
{
    public class Counter
    {
        public CountingResult Count(Ballot ballot)
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot(ballot.GetSelection());
            return countingResult;
        }
    }
}