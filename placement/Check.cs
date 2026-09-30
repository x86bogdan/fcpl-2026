namespace Placement;

/// <summary>
/// A very small check runner. In session 3 you will meet the real thing (xUnit),
/// and in session 12 you will build one of these yourself. You do not need to change this file.
/// </summary>
public static class Check
{
    private static int _passed;
    private static int _total;

    public static void Equal<T>(string label, T expected, Func<T> actual)
        => Run(label, expected, actual, (a, b) => EqualityComparer<T>.Default.Equals(a, b), Show);

    public static void Sequence<T>(string label, IEnumerable<T> expected, Func<IEnumerable<T>> actual)
        => Run(label, expected, actual, (a, b) => a.SequenceEqual(b), s => "[" + string.Join(", ", s) + "]");

    public static void Map<TKey, TValue>(
        string label,
        IReadOnlyDictionary<TKey, TValue> expected,
        Func<IReadOnlyDictionary<TKey, TValue>> actual) where TKey : notnull
        => Run(label, expected, actual,
               (a, b) => a.Count == b.Count && a.All(kv => b.TryGetValue(kv.Key, out var v)
                                                          && EqualityComparer<TValue>.Default.Equals(kv.Value, v)),
               m => "{" + string.Join(", ", m.Select(kv => $"{kv.Key}={kv.Value}")) + "}");

    private static void Run<T>(string label, T expected, Func<T> actual,
                               Func<T, T, bool> areEqual, Func<T, string> show)
    {
        _total++;
        try
        {
            var got = actual();
            if (got is not null && areEqual(expected, got))
            {
                _passed++;
                Write(ConsoleColor.Green, "  PASS  ");
                Console.WriteLine(label);
            }
            else
            {
                Write(ConsoleColor.Red, "  FAIL  ");
                Console.WriteLine($"{label}");
                Console.WriteLine($"          expected {show(expected)}");
                Console.WriteLine($"          got      {(got is null ? "null" : show(got))}");
            }
        }
        catch (NotImplementedException)
        {
            Write(ConsoleColor.DarkGray, "  TODO  ");
            Console.WriteLine(label);
        }
        catch (Exception ex)
        {
            Write(ConsoleColor.Red, "  FAIL  ");
            Console.WriteLine($"{label}");
            Console.WriteLine($"          threw {ex.GetType().Name}: {ex.Message}");
        }
    }

    public static void Report()
    {
        Console.WriteLine();
        Console.WriteLine(new string('-', 52));
        var colour = _passed == _total ? ConsoleColor.Green
                   : _passed == 0 ? ConsoleColor.Red
                   : ConsoleColor.Yellow;
        Write(colour, $"  {_passed} of {_total} checks passing  ");
        Console.WriteLine();
        Console.WriteLine();
        Console.WriteLine(_passed switch
        {
            <= 2 => "  Guided route. You will be glad of the scaffolding.",
            <= 5 => "  Guided route. You have the basics; the labs will fill in the rest.",
            <= 7 => "  Either route works. Pick guided if you want the structure.",
            _    => "  Project route is worth considering. Come and talk to me."
        });
        Console.WriteLine();
        Console.WriteLine("  This is advice, not an assignment. You choose your own route.");
        Console.WriteLine();
    }

    private static string Show<T>(T value) => value?.ToString() ?? "null";

    private static void Write(ConsoleColor colour, string text)
    {
        var previous = Console.ForegroundColor;
        Console.ForegroundColor = colour;
        Console.Write(text);
        Console.ForegroundColor = previous;
    }
}
