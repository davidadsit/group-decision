using System.Collections.Generic;
using System.Linq;

namespace GroupDecisionMaker
{
    public class CountingResult
    {
        readonly Dictionary<string, int> selections = new Dictionary<string, int>();

        public string[] TopCandidates
        {
            get
            {
                if (selections.Count == 0)
                {
                    return new string[] {};
                }
                var topSelectionCount = selections.Values.Max();
                return selections.Keys.Where(x => selections[x] == topSelectionCount).ToArray();
            }
        }

        public int TotalVotes => selections.Values.Sum();
        public string[] AllCandidates => selections.Keys.ToArray();

        public string[] BottomCandidates
        {
            get
            {
                if (selections.Count == 0)
                {
                    return new string[] {};
                }
                var topSelectionCount = selections.Values.Min();
                return selections.Keys.Where(x => selections[x] == topSelectionCount).ToArray();
            }
        }

        public void RecordBallot(string selection)
        {
            if(selection == null) return;
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
            if (selections.Count > 0 && selections.ContainsKey(selection))
            {
                return selections[selection];
            }
            return 0;
        }
    }
}