using System.Linq;

namespace GroupDecisionMaker
{
    public class Ballot
    {
        readonly string[] selections;

        public Ballot(params string[] selections)
        {
            this.selections = selections;
        }

        public string GetSelection(params string[] exclusions)
        {
            return selections.FirstOrDefault(x => exclusions.All(e => e != x));
        }

        public override string ToString()
        {
            return string.Join(" -> ", selections);
        }
    }
}