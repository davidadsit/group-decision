using System.Collections.Generic;

namespace GroupDecisionMaker
{
    public class Counter
    {
        public CountingResult Count(params Ballot[] ballots)
        {
            var countingResult = new CountingResult();
            foreach (var ballot in ballots)
            {
                countingResult.RecordBallot(ballot.GetSelection());
            }
            return countingResult;
        }
    }
}