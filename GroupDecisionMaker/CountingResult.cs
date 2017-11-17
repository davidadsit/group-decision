namespace GroupDecisionMaker
{
    public class CountingResult
    {
        public string Winner { get; private set; }

        public void RecordBallot(string selection)
        {
            Winner = selection;
        }
    }
}