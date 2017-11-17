using System.Collections.Generic;
using System.Linq;

namespace GroupDecisionMaker
{
    public class CountingResult
    {
        readonly Dictionary<string, int> selections = new Dictionary<string, int>();

        public string[] TopSelections
        {
            get
            {
                if (selections.Count == 0)
                {
                    return new string[]{};
                }
                var topSelectionCount = selections.Values.Max();
                return selections.Keys.Where(x => selections[x] == topSelectionCount).ToArray();
            }
        }

        public int TotalVotes => selections.Values.Sum();

        public void RecordBallot(string selection)
        {
            if (selections.ContainsKey(selection))
            {
                selections[selection]++;
            }
            else
            {
                selections.Add(selection, 1);
            }
        }

        public int Votes(string selection)
        {
            return selections.ContainsKey(selection) ? selections[selection] : 0;
        }
    }
}