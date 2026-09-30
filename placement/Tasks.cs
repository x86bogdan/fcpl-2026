namespace Placement;

/// <summary>
/// THIS IS THE ONLY FILE YOU NEED TO EDIT.
///
/// Replace each `throw new NotImplementedException();` with a real implementation.
/// Run `dotnet run` to see how you are doing. Get as far as you can in 20 minutes —
/// nobody is expected to finish, and this is not graded.
/// </summary>
public static class Tasks
{
    // ---- Level 1 -------------------------------------------------------

    /// <summary>Returns "Welcome, Ada!" for the name "Ada".</summary>
    public static string Greet(string name)
        => throw new NotImplementedException();

    /// <summary>True when the adventurer is level 10 or above.</summary>
    public static bool IsVeteran(Adventurer adventurer)
        => throw new NotImplementedException();

    // ---- Level 2 -------------------------------------------------------

    /// <summary>How many members of the party have the given class.</summary>
    public static int CountInClass(IEnumerable<Adventurer> party, string className)
        => throw new NotImplementedException();

    /// <summary>The sum of every member's level.</summary>
    public static int TotalLevels(IEnumerable<Adventurer> party)
        => throw new NotImplementedException();

    // ---- Level 3 -------------------------------------------------------

    /// <summary>
    /// Every member's name, highest level first.
    /// Members on the same level keep the order they appear in the party.
    /// </summary>
    public static IEnumerable<string> NamesByLevelDescending(IEnumerable<Adventurer> party)
        => throw new NotImplementedException();

    /// <summary>
    /// Finds the single highest-level member.
    /// Returns false (and null) when the party is empty, rather than throwing.
    /// </summary>
    public static bool TryFindStrongest(IEnumerable<Adventurer> party, out Adventurer? strongest)
        => throw new NotImplementedException();

    // ---- Level 4 -------------------------------------------------------

    /// <summary>How many members there are of each class, keyed by class name.</summary>
    public static IReadOnlyDictionary<string, int> CountByClass(IEnumerable<Adventurer> party)
        => throw new NotImplementedException();

    /// <summary>
    /// The average level of each class, keyed by class name,
    /// rounded to one decimal place.
    /// </summary>
    public static IReadOnlyDictionary<string, double> AverageLevelByClass(IEnumerable<Adventurer> party)
        => throw new NotImplementedException();

    // ---- Still here? ---------------------------------------------------

    /// <summary>
    /// A leaderboard, highest level first, formatted as "1. Ada (Mage, 14)".
    /// Members on the same level share a rank, and the next rank skips
    /// accordingly — so two members on rank 2 are followed by rank 4.
    /// </summary>
    public static IEnumerable<string> Leaderboard(IEnumerable<Adventurer> party)
        => throw new NotImplementedException();
}
