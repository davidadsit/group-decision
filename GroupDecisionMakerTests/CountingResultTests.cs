using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class CountingResultTests
    {
        [Fact]
        public void Winner_with_one_vote()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");

            Assert.Equal(new[] {"Blue"}, countingResult.Winner);
            Assert.Equal(1, countingResult.Votes("Blue"));
            Assert.Equal(0, countingResult.Votes("Green"));
        }

        [Fact]
        public void Winner_with_two_single_vote_selections()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Green");

            Assert.Equal(new[] {"Blue", "Green"}, countingResult.Winner);
            Assert.Equal(1, countingResult.Votes("Blue"));
            Assert.Equal(1, countingResult.Votes("Green"));
        }

        [Fact]
        public void Winner_with_two_votes()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Blue");

            Assert.Equal(new[] {"Blue"}, countingResult.Winner);
            Assert.Equal(2, countingResult.Votes("Blue"));
        }

        [Fact]
        public void Winner_with_three_votes()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Green");

            Assert.Equal(new[] {"Blue"}, countingResult.Winner);
        }
    }
}