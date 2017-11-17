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

            var instantRunOff = new InstantRunOff(new OnePersonOneVote());
            var plurality = new Plurality(new OnePersonOneVote());
            File.WriteAllText("election.log", $"Election Results{Environment.NewLine}{Environment.NewLine}");
            for (var i = 0; i < 100; i++)
            {
                var ballot = RandomBallot();
                File.AppendAllText("election.log", $"{i:00} => {ballot}{Environment.NewLine}");
                instantRunOff.RecordBallot(i.ToString(), ballot);
                plurality.RecordBallot(i.ToString(), ballot);
            }
            File.AppendAllText("election.log", $"{Environment.NewLine}{Environment.NewLine}IRV Results{Environment.NewLine}{instantRunOff.BuildReport().GetTextReportText()}");
            File.AppendAllText("election.log", $"{Environment.NewLine}{Environment.NewLine}Plurality Results{Environment.NewLine}{plurality.BuildReport().GetTextReportText()}");
            Console.Out.WriteLine("Open election.log to see results");
        }

        static Ballot RandomBallot()
        {
            return new Ballot(AllCandidates.OrderBy(x => Guid.NewGuid()).Take(Random.Next(2, 7)).ToArray());
        }
    }
}