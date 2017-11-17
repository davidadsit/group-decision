using GroupDecisionMaker;
using GroupDecisionMaker.BallotCollectors;
using GroupDecisionMaker.VotingSystems;
using Xunit;

namespace GroupDecisionMakerTests
{
    public class InstantRunOffTests
    {
        public class FiveCandidateTests
        {
            public FiveCandidateTests()
            {
                var votingSystem = new InstantRunOff(new OnePersonOneVote());

                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Green", "Blue"));
                votingSystem.RecordBallots(new Ballot("Green", "Blue"));
                votingSystem.RecordBallots(new Ballot("Green", "Blue"));
                votingSystem.RecordBallots(new Ballot("Purple", "Yellow", "Blue"));

                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Yellow", "Red"));
                votingSystem.RecordBallots(new Ballot("Yellow", "Red"));

                report = votingSystem.BuildReport();
            }

            readonly VotingReport report;

            [Fact]
            public void Report_includes_vote_totals()
            {
                var reportText = report.GetTextReportText();

                Assert.Contains("Blue had 8 votes", reportText);
                Assert.Contains("Red had 6 votes", reportText);
                Assert.Contains("Purple had 1 votes and will be excluded from future rounds", reportText);
                Assert.Contains("Green had 3 votes and will be excluded from future rounds", reportText);
                Assert.Contains("Yellow had 3 votes and will be excluded from future rounds", reportText);
            }

            [Fact]
            public void Report_includes_winner()
            {
                Assert.Equal("Blue", report.Winner);
                Assert.Contains("Blue wins!", report.GetTextReportText());
            }
        }

        public class ThreeCandidateTests
        {
            public ThreeCandidateTests()
            {
                var votingSystem = new InstantRunOff(new OnePersonOneVote());

                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Green", "Blue"));

                report = votingSystem.BuildReport();
            }

            readonly VotingReport report;

            [Fact]
            public void Report_includes_vote_totals()
            {
                var reportText = report.GetTextReportText();

                Assert.Contains("Blue had 4 votes", reportText);
                Assert.Contains("Red had 3 votes", reportText);
                Assert.Contains("Green had 1 votes and will be excluded from future rounds", reportText);
            }

            [Fact]
            public void Report_includes_winner()
            {
                Assert.Equal("Blue", report.Winner);
                Assert.Contains("Blue wins!", report.GetTextReportText());
            }
        }

        public class TwoCandidateTests
        {
            public TwoCandidateTests()
            {
                var votingSystem = new InstantRunOff(new OnePersonOneVote());

                votingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Red"), new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));

                report = votingSystem.BuildReport();
            }

            readonly VotingReport report;

            [Fact]
            public void Report_includes_vote_totals()
            {
                var reportText = report.GetTextReportText();

                Assert.Contains("7 votes were recorded", reportText);
                Assert.Contains("Blue had 4 votes", reportText);
                Assert.Contains("Red had 3 votes", reportText);
            }

            [Fact]
            public void Report_includes_winner()
            {
                var reportText = report.GetTextReportText();

                Assert.Equal("Blue", report.Winner);
                Assert.Contains("Blue wins!", reportText);
            }
        }

        public class TiedRaces
        {
            [Fact]
            public void Three_candidates()
            {
                var votingSystem = new InstantRunOff(new OnePersonOneVote());
                votingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));
                votingSystem.RecordBallots(new Ballot("Green"), new Ballot("Green"));

                Assert.Equal("Tie", votingSystem.BuildReport().Winner);
            }

            [Fact]
            public void Two_candidates()
            {
                var votingSystem = new InstantRunOff(new OnePersonOneVote());
                votingSystem.RecordBallots(new Ballot("Blue"), new Ballot("Blue"));
                votingSystem.RecordBallots(new Ballot("Red"), new Ballot("Red"));

                Assert.Equal("Tie", votingSystem.BuildReport().Winner);
            }
        }

        [Fact]
        public void PluralityVotingSystem_when_no_one_comes_out()
        {
            var votingSystem = new InstantRunOff(new OnePersonOneVote());

            Assert.Equal("Inconclusive", votingSystem.BuildReport().Winner);
        }
    }
}