using WordleSolver.IO;

namespace WordleSolver.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            var words = GetWords();
            var solver = new Solver(words);

            while (true)
            {
                Console.Write(": ");
                var input = Console.ReadLine();
                if (!solver.ValidateInput(input))
                {
                    Console.WriteLine("Invalid input");
                    continue;
                }
                var suggestions = solver.GetSuggestions(input);
                var joined = string.Join(' ', suggestions);
                Console.WriteLine(joined);
            }
        }

        static string[] GetWords()
        {
            var path = Path.GetFullPath(".\\Resources\\word_list.txt");
            var words = File.ReadAllText(path);

            return words.Split(Environment.NewLine);
        }
    }
}