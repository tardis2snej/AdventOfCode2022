using System.Text;

namespace Day1_CaloriesCounting
{
    internal static class Program
    {
        private const string INPUT_PATH = @"D:\AdventOfCode2022\Day1_CaloriesCounting\input.txt";
        
        static void Main(string[] args)
        {
            IEnumerable<string> enumLines = File.ReadLines(Path.Combine(INPUT_PATH), Encoding.UTF8);

            List<int> topCalories = new List<int> {0, 0, 0};
            int currentCalories = 0;
            
            foreach (string line in enumLines)
            {
                if (int.TryParse(line, out int calories))
                {
                    currentCalories += calories;
                }
                else
                {
                    for (int i = 0; i < topCalories.Count; i++)
                    {
                        if (currentCalories > topCalories[i])
                        {
                            topCalories.Insert(i, currentCalories);
                            topCalories.RemoveAt(topCalories.Count - 1);
                            break;
                        }
                    }
                    
                    currentCalories = 0;
                }
            }
            
            Console.WriteLine($"Max calories is {topCalories[0]}");
            Console.WriteLine($"Total of 3 top elves is {topCalories.Sum(x => x)}");
        }
    }
}