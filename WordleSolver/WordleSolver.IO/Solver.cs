namespace WordleSolver.IO;

public class Solver
{
    private readonly List<char> invalidLetters = new();
    private readonly List<char> validLetters = new();
    private readonly Letter[] letters = new Letter[5] { new(), new(), new(), new(), new() };
    private readonly string[] words;

    /// <summary>
    /// Initializes new instance with guess words.
    /// </summary>
    /// <param name="words">Possible word solutions.</param>
    public Solver(string[] words)
    {
        this.words = words;
    }

    /// <summary>
    /// Simple input validation.
    /// </summary>
    /// <param name="input">Input to be validated.</param>
    /// <returns></returns>
    public bool ValidateInput(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return false;
        }

        var lettersOnly = this.Replace(input);

        return lettersOnly.Length == 5;
    }

    /// <summary>
    /// Get suggestions based on provided inputs.
    /// </summary>
    /// <param name="input">Current input to be processed.</param>
    /// <returns></returns>
    public List<string> GetSuggestions(string input)
    {
        input = input.ToUpperInvariant();

        using (var enumerator = input.GetEnumerator())
        {
            var index = 0;
            while (enumerator.MoveNext())
            {
                if (enumerator.Current == '[')
                {
                    enumerator.MoveNext();
                    _ = this.invalidLetters.Remove(enumerator.Current);
                    this.validLetters.Add(enumerator.Current);
                    this.letters[index].RightCharacter = enumerator.Current;
                    enumerator.MoveNext();
                }
                else if (enumerator.Current == '(')
                {
                    enumerator.MoveNext();
                    _ = this.invalidLetters.Remove(enumerator.Current);
                    this.validLetters.Add(enumerator.Current);
                    this.letters[index].InvalidCharacters.Add(enumerator.Current);
                    enumerator.MoveNext();
                }
                else
                {
                    var isTaken = letters.Any(x => x.RightCharacter == enumerator.Current);
                    if (!isTaken)
                    {
                        this.invalidLetters.Add(enumerator.Current);
                    }
                }

                index++;
            }

        }

        input = this.Replace(input);

        return words.Where(x =>
        {
            for (var i = 0; i < x.Length; i++)
            {
                if (invalidLetters.Contains(x[i]))
                {
                    return false;
                }
                if (letters[i].InvalidCharacters.Contains(x[i]))
                {
                    return false;
                }
                if (letters[i].RightCharacter is not null && letters[i].RightCharacter != x[i])
                {
                    return false;
                }
            }

            return this.validLetters.All(validLetter => x.Contains(validLetter));
        }).ToList();
    }

    /// <summary>
    /// Replace utility characters.
    /// </summary>
    /// <param name="input">Input to be replaced.</param>
    /// <returns></returns>
    private string Replace(string input)
    {
        return input.Replace("[", string.Empty)
                   .Replace("]", string.Empty)
                   .Replace("(", string.Empty)
                   .Replace(")", string.Empty);
    }
}
