using System;
using System.IO;
using System.Linq;
using GroupDecisionMaker;
using GroupDecisionMaker.BallotCollectors;
using GroupDecisionMaker.VotingSystems;

namespace VotingMachine
{
    class Program
    {
        static readonly Random Random = new Random();
        static readonly string[] AllCandidates = {"Red", "Blue", "Green", "Yellow", "Orange", "Purple"};

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Group Decision Voting Machine!");

            var votingSystem = new InstantRunOff(new OnePersonOneVote());
            File.WriteAllText("election.log", $"Election Results{Environment.NewLine}{Environment.NewLine}");
            for (var i = 0; i < 100; i++)
            {
                var ballot = RandomBallot();
                File.AppendAllText("election.log", $"{i:00} => {ballot}{Environment.NewLine}");
                votingSystem.RecordBallot(i.ToString(), ballot);
            }
            var votingReport = votingSystem.BuildReport();
            var reportText = votingReport.GetTextReportText();
            Console.Out.WriteLine(reportText);
            File.AppendAllText("election.log", $"{Environment.NewLine}{reportText}");
        }

        static Ballot RandomBallot()
        {
            return new Ballot(AllCandidates.OrderBy(x => Guid.NewGuid()).Take(Random.Next(2, 7)).ToArray());
        }
    }
}