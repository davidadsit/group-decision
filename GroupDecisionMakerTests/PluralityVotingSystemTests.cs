using System;
using GroupDecisionMaker;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class PluralityVotingSystemTests
    {
        [Fact]
        public void PluralityVotingSystem_two_candidate_race()
        {
            var pluralityVotingSystem = new PluralityVotingSystem();
            pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Red"), new Ballot("Blue"));
            pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
            pluralityVotingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));

            Assert.Equal("Blue", pluralityVotingSystem.Winner);
        }

        [Fact]
        public void PluralityVotingSystem_two_candidate_race_report()
        {
            var pluralityVotingSystem = new PluralityVotingSystem();
            pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Red"), new Ballot("Blue"));
            pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
            pluralityVotingSystem.RecordBallots(new Ballot("Yellow"), new Ballot("Red"));
            pluralityVotingSystem.RecordBallots(new Ballot("Yellow"), new Ballot("Red"));
            pluralityVotingSystem.RecordBallots(new Ballot("Green"));

            var report = pluralityVotingSystem.BuildReport();
            Assert.Contains("Blue wins!",report);
            Assert.Contains("Blue had 4 votes",report);
            Assert.Contains("Red had 3 votes",report);
            Assert.Contains("Yellow had 2 votes",report);
            Assert.Contains("Green had 1 vote",report);
        }

        [Fact]
        public void PluralityVotingSystem_tie_race()
        {
            var pluralityVotingSystem = new PluralityVotingSystem();
            pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
            pluralityVotingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));

            Assert.Equal("Tie", pluralityVotingSystem.Winner);
        }

        [Fact]
        public void PluralityVotingSystem_when_no_one_comes_out()
        {
            var pluralityVotingSystem = new PluralityVotingSystem();

            Assert.Equal("Inconclusive", pluralityVotingSystem.Winner);
        }
    }
}