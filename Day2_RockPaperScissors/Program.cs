using System.Text;

namespace Day2_RockPaperScissors
{
    internal static partial class Program
    {
        private const string INPUT_PATH = @"D:\AdventOfCode\Day2_RockPaperScissors\input.txt";

        private static Dictionary<char, Shape> symbolShapeOpponentMap = new()
        {
            { 'A', Shape.Rock },
            { 'B', Shape.Paper },
            { 'C', Shape.Scissors }
        };
        
        private static Dictionary<char, Shape> symbolShapeMyMap = new()
        {
            { 'X', Shape.Rock },
            { 'Y', Shape.Paper },
            { 'Z', Shape.Scissors }
        };

        private static Dictionary<char, Outcome> symbolOutcomeMap = new()
        {
            { 'X', Outcome.Lose },
            { 'Y', Outcome.Draw },
            { 'Z', Outcome.Win }
        };

        static void Main(string[] args)
        {
            IEnumerable<string> enumLines = File.ReadLines(Path.Combine(INPUT_PATH), Encoding.UTF8);

            int scorePart1 = 0, scorePart2 = 0;
            foreach (string line in enumLines)
            {
                {
                    // First part of task: X, Y, Z - my reply
                    (Shape opponentMove, Shape myMove) = DecodeMoves(line);
                    scorePart1 += (int)myMove + (int)GetOutcome(opponentMove, myMove);
                }
                
                {
                    // Second part of task: X, Y, Z - outcomes
                    (Shape opponentMove, Outcome outcome) = DecodeOutcomes(line);
                    Shape myMove = Shape.Undefined; 
                    for (int i = 1; i <= 3; i++)
                    {
                        if (GetOutcome(opponentMove, (Shape)i) == outcome)
                        {
                            myMove = (Shape)i;
                            break;
                        }
                    }
                    scorePart2 += (int)myMove + (int)GetOutcome(opponentMove, myMove);
                }
            }
            
            Console.WriteLine($"Total score part 1: {scorePart1}");
            Console.WriteLine($"Total score part 2: {scorePart2}");
        }

        private static (Shape, Shape) DecodeMoves(string strategyLine)
        {
            Shape opponentMove = Shape.Undefined, myMove = Shape.Undefined;
            foreach (char c in strategyLine)
            {
                if (opponentMove == Shape.Undefined && symbolShapeOpponentMap.TryGetValue(c, out opponentMove))
                {
                    continue;
                }

                if (myMove == Shape.Undefined && symbolShapeMyMap.TryGetValue(c, out myMove))
                {
                    break;
                }
            }
            
            if (opponentMove == Shape.Undefined || myMove == Shape.Undefined)
            {
                Console.WriteLine("Error: undefined shape.");
            }

            return (opponentMove, myMove);
        }

        private static (Shape, Outcome) DecodeOutcomes(string strategyLine)
        {
            Shape opponentMove = Shape.Undefined;
            Outcome outcome = Outcome.Undefined;
            foreach (char c in strategyLine)
            {
                if (opponentMove == Shape.Undefined && symbolShapeOpponentMap.TryGetValue(c, out opponentMove))
                {
                    continue;
                }

                if (outcome == Outcome.Undefined)
                {
                    if (symbolOutcomeMap.TryGetValue(c, out outcome))
                    {
                        break;
                    }
                    else
                    {
                        outcome = Outcome.Undefined;
                    }
                }
            }
            
            if (opponentMove == Shape.Undefined || outcome == Outcome.Undefined)
            {
                Console.WriteLine("Error: undefined symbol.");
            }

            return (opponentMove, outcome);
        }

        static Outcome GetOutcome(Shape opponentMove, Shape myMove)
        {
            if (opponentMove == myMove)
            {
                return Outcome.Draw;
            }

            if ((opponentMove == Shape.Rock && myMove == Shape.Paper) ||
                (opponentMove == Shape.Paper && myMove == Shape.Scissors) ||
                (opponentMove == Shape.Scissors && myMove == Shape.Rock))
            {
                return Outcome.Win;
            }

            return Outcome.Lose;
        }
    }
}