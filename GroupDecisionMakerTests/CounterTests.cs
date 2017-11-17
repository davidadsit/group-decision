using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class CounterTests
    {
        [Fact]
        public void When_counting_a_single_ballot()
        {
            var counter = new Counter();
            var result = counter.Count(new Ballot("Red"));

            Assert.Equal(new[] {"Red"}, result.TopSelections);
        }

        [Fact]
        public void When_counting_several_ballots()
        {
            var counter = new Counter();
            var result = counter.Count(
                new Ballot("Red"),
                new Ballot("Red"),
                new Ballot("Red"),
                new Ballot("Blue")
            );

            Assert.Equal(new[] {"Red"}, result.TopSelections);
        }
    }
}