using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class BallotTests
    {
        [Fact]
        public void Top_selection_with_single_choice()
        {
            var ballot = new Ballot("Red");

            Assert.Equal("Red", ballot.GetSelection());
        }

        [Fact]
        public void Top_selection_with_multiple_choices()
        {
            var ballot = new Ballot("Blue", "Green", "Red");

            Assert.Equal("Blue", ballot.GetSelection());
        }
    }
}