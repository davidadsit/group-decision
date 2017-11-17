using System.Collections.Generic;
using System.Linq;

namespace GroupDecisionMaker
{
    public class CountingResult
    {
        readonly Dictionary<string, int> selections = new Dictionary<string, int>();

        public string[] Winner => selections.Keys.ToArray();

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