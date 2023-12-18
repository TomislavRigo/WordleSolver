namespace WordleSolver.IO;

/// <summary>
/// Letter character model.
/// </summary>
public class Letter
{
    /// <summary>
    /// List of characters not on this letter position.
    /// </summary>
    public List<char> InvalidCharacters { get; set; } = new();

    /// <summary>
    /// Character on this letter position.
    /// </summary>
    public char? RightCharacter { get; set; }
}
