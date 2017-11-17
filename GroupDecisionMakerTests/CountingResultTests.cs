using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class CountingResultTests
    {
        [Fact]
        public void TopSelections_with_no_votes()
        {
            var countingResult = new CountingResult();

            Assert.Equal(new string[] {}, countingResult.TopCandidates);
            Assert.Equal(0, countingResult.Votes("Green"));
            Assert.Equal(0, countingResult.TotalVotes);
        }

        [Fact]
        public void TopSelections_with_one_vote()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");

            Assert.Equal(new[] {"Blue"}, countingResult.TopCandidates);
            Assert.Equal(1, countingResult.Votes("Blue"));
            Assert.Equal(0, countingResult.Votes("Green"));
            Assert.Equal(1, countingResult.TotalVotes);
        }

        [Fact]
        public void TopSelections_with_two_single_vote_selections()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Green");

            Assert.Equal(new[] {"Blue", "Green"}, countingResult.TopCandidates);
            Assert.Equal(1, countingResult.Votes("Blue"));
            Assert.Equal(1, countingResult.Votes("Green"));
            Assert.Equal(2, countingResult.TotalVotes);
        }

        [Fact]
        public void TopSelections_with_two_votes()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Blue");

            Assert.Equal(new[] {"Blue"}, countingResult.TopCandidates);
            Assert.Equal(2, countingResult.Votes("Blue"));
            Assert.Equal(2, countingResult.TotalVotes);
        }

        [Fact]
        public void TopSelections_with_three_votes()
        {
            var countingResult = new CountingResult();
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Blue");
            countingResult.RecordBallot("Green");

            Assert.Equal(new[] {"Blue"}, countingResult.TopCandidates);
            Assert.Equal(3, countingResult.TotalVotes);
            Assert.Equal(new[] { "Blue", "Green" }, countingResult.AllCandidates);
        }
    }
}