using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class CounterTests
    {
        [Fact]
        public void When_counting_a_single_ballot()
        {
            var ballot = new Ballot("Red");
            var counter = new Counter();
            var result = counter.Count(ballot);

            Assert.Equal(result.Winner, "Red");
        }
    }
}