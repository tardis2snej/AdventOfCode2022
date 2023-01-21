using System.Text;

namespace Day1_CaloriesCounting
{
    internal static class Program
    {
        private const string INPUT_PATH = @"D:\AdventOfCode2022\Day3_RucksackReorganization\input.txt";
        
        static void Main(string[] args)
        {
            IEnumerable<string> enumLines = File.ReadLines(Path.Combine(INPUT_PATH), Encoding.UTF8);

            int prioritiesSum = 0, identifiersSum = 0, rucksackIndex = 0;
            string[] rucksackTempBuffer = new string[3];
            foreach (var line in enumLines)
            {
                // Part 1 - Priorities of duplicated item in compartments
                {
                    if (line.Length % 2 != 0)
                    {
                        Console.WriteLine("Can't split line to half.");
                        continue;
                    }

                    string firstComp = line.Substring(0, line.Length / 2);
                    string secondComp = line.Substring(line.Length / 2);

                    foreach (char item in firstComp)
                    {
                        if (secondComp.Contains(item))
                        {
                            prioritiesSum += GetPriority(item);
                            break;
                        }
                    }
                }
                
                // Part 2 - Priorities if item-identifiers of each group
                {
                    rucksackTempBuffer[rucksackIndex % 3] = line;
                    if (rucksackIndex % 3 == 2)
                    {
                        char identifier = Char.MinValue;
                        foreach (char item in rucksackTempBuffer[0])
                        {
                            identifier = item;
                            for (int i = 1; i < rucksackTempBuffer.Length; i++)
                            {
                                if (!rucksackTempBuffer[i].Contains(identifier))
                                {
                                    identifier = Char.MinValue;
                                    break;
                                }
                            }

                            if (identifier != char.MinValue)
                            {
                                break;
                            }
                        }

                        identifiersSum += GetPriority(identifier);
                    }
                }

                rucksackIndex++;
            }
            
            Console.WriteLine($"Priorities sum is {prioritiesSum}");
            Console.WriteLine($"Priorities group sum is {identifiersSum}");
        }

        private static int GetPriority(char item)
        {
            int priority;
            if (char.IsUpper(item))
            {
                priority = item - 38;
            }
            else
            {
                priority = item - 96;
            }

            return priority;
        }
    }
}