using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class BallotSelectionTests
    {
        public class When_a_ballot_has_a_single_selection
        {
            [Fact]
            public void That_selection_is_returned()
            {
                var ballot = new Ballot("Red");

                Assert.Equal("Red", ballot.GetSelection());
            }
        }

        public class When_a_ballot_has_multiple_selections
        {
            [Fact]
            public void The_first_selection_is_returned()
            {
                var ballot = new Ballot("Blue", "Green", "Red");

                Assert.Equal("Blue", ballot.GetSelection());
            }

            [Fact]
            public void When_there_are_excusions_the_first_nonexcluded_selection_is_returned()
            {
                var ballot = new Ballot("Blue", "Green", "Red");

                Assert.Equal("Green", ballot.GetSelection("Blue"));
            }
        }
    }
}