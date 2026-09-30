using Placement;

var party = new List<Adventurer>
{
    new("Ada",   "Mage",   14),
    new("Bjorn", "Warrior", 9),
    new("Cleo",  "Rogue",  14),
    new("Dara",  "Mage",    6),
    new("Eren",  "Warrior", 9),
};

Console.WriteLine();
Console.WriteLine("  FCPL lab (SharpQuest) — placement task");
Console.WriteLine("  Edit Tasks.cs, then run `dotnet run` again.");
Console.WriteLine();

Check.Equal("1.  Greet returns a welcome", "Welcome, Ada!", () => Tasks.Greet("Ada"));
Check.Equal("2.  IsVeteran is true at level 14", true, () => Tasks.IsVeteran(party[0]));
Check.Equal("2b. IsVeteran is false at level 9", false, () => Tasks.IsVeteran(party[1]));

Check.Equal("3.  CountInClass finds 2 mages", 2, () => Tasks.CountInClass(party, "Mage"));
Check.Equal("4.  TotalLevels adds up to 52", 52, () => Tasks.TotalLevels(party));

Check.Sequence("5.  NamesByLevelDescending", new[] { "Ada", "Cleo", "Bjorn", "Eren", "Dara" },
    () => Tasks.NamesByLevelDescending(party));
Check.Equal("6.  TryFindStrongest finds Ada", "Ada",
    () => Tasks.TryFindStrongest(party, out var s) ? s!.Name : "(not found)");
Check.Equal("6b. TryFindStrongest survives an empty party", false,
    () => Tasks.TryFindStrongest(new List<Adventurer>(), out _));

Check.Map("7.  CountByClass", new Dictionary<string, int> { ["Mage"] = 2, ["Warrior"] = 2, ["Rogue"] = 1 },
    () => Tasks.CountByClass(party));
Check.Map("8.  AverageLevelByClass", new Dictionary<string, double> { ["Mage"] = 10.0, ["Warrior"] = 9.0, ["Rogue"] = 14.0 },
    () => Tasks.AverageLevelByClass(party));

Check.Sequence("9.  Leaderboard shares and skips ranks",
    new[] { "1. Ada (Mage, 14)", "1. Cleo (Rogue, 14)", "3. Bjorn (Warrior, 9)", "3. Eren (Warrior, 9)", "5. Dara (Mage, 6)" },
    () => Tasks.Leaderboard(party));

Check.Report();
