using GroupDecisionMaker;
using GroupDecisionMaker.BallotCollectors;
using GroupDecisionMaker.VotingSystems;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class PluralityVotingSystemTests
    {
        public class TwoCandidateTests
        {
            [Fact]
            public void PluralityVotingSystem_two_candidate_race_report()
            {
                var pluralityVotingSystem = new PluralityVotingSystem(new OnePersonOneVote());
                pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Red"), new Ballot("Blue"));
                pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
                pluralityVotingSystem.RecordBallots(new Ballot("Yellow"), new Ballot("Red"));
                pluralityVotingSystem.RecordBallots(new Ballot("Yellow"), new Ballot("Red"));
                pluralityVotingSystem.RecordBallots(new Ballot("Green"));

                var report = pluralityVotingSystem.BuildReport();
                var reportText = report.GetTextReportText();
                Assert.Equal("Blue", report.Winner);
                Assert.Contains("Blue wins!", reportText);
                Assert.Contains("Blue had 4 votes", reportText);
                Assert.Contains("Red had 3 votes", reportText);
                Assert.Contains("Yellow had 2 votes", reportText);
                Assert.Contains("Green had 1 vote", reportText);
            }
        }

        public class TiedRaces
        {
            [Fact]
            public void Three_candidates()
            {
                var pluralityVotingSystem = new PluralityVotingSystem(new OnePersonOneVote());
                pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
                pluralityVotingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));
                pluralityVotingSystem.RecordBallots(new Ballot("Green"), new Ballot("Green"));

                Assert.Equal("Tie", pluralityVotingSystem.BuildReport().Winner);
            }

            [Fact]
            public void Two_candidates()
            {
                var pluralityVotingSystem = new PluralityVotingSystem(new OnePersonOneVote());
                pluralityVotingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
                pluralityVotingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));

                Assert.Equal("Tie", pluralityVotingSystem.BuildReport().Winner);
            }
        }

        [Fact]
        public void PluralityVotingSystem_when_no_one_comes_out()
        {
            var pluralityVotingSystem = new PluralityVotingSystem(new OnePersonOneVote());

            Assert.Equal("Inconclusive", pluralityVotingSystem.BuildReport().Winner);
        }
    }
}