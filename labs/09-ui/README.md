# Lab 09 — A window

**Today:** the same model, the same store, and something you can click. Plus the idea that makes a user interface testable at all.

**Required feature (matrix row 9):** a working window bound to your model (WPF or Avalonia)

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **9** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

---

## 0. Start (5 min)

**On a lab PC (browser only, no git):** your repository on GitHub → **Code → Download ZIP** → extract it → open the folder in Rider. Then, in **Rider's terminal** (**Alt+F12**):

```
sh sq.sh update
sh sq.sh test
```

**On your laptop, with git:** in your repository folder,

```
git pull
sq update          (Linux/macOS: sh sq.sh update)
sq test
```

Lab 09 shows as *not started*; Labs 02–08 should still be green.

**Project route: today is checkpoint 3**, covering rows S7–S9. See `CHECKPOINT-3.md`.

**Looking for the syntax?** The [C# phrasebook](https://x86bogdan.github.io/fcpl-2026/cheatsheet/#lab09) has today's C# next to Java and C++, with the errors you're likely to meet.

## 1. Why the tests can't see your window (10 min)

They can't, and they never will. The checks that grade you run on a build server in the cloud, on Linux, with no screen — a WPF project can't even be *built* there. So if "a working window" were the graded thing, there would be nothing to grade.

Which turns out to be the best possible reason to learn the pattern that professionals use anyway:

> **Everything about what the screen shows lives in a class that has never heard of a Button.**

That class is a **view model**. It holds the list, the search text, the selection, the description, the two commands. The window holds bindings and nothing else. And because the view model is ordinary C#, a test can drive your entire interface without a window existing.

So today splits cleanly:

| Part | Where it lives | How it's graded |
|---|---|---|
| The view model | `src/Capstone.Core/ViewModels/` | 43 automatic checks |
| The window | `src/Capstone.Ui/` | you show it to me, in the room |

**Either framework is fine.** WPF if you're on Windows with Visual Studio, Avalonia anywhere. The XAML differs by one namespace line. Nothing in `Capstone.Core` changes either way — and the tests check that too: your domain library must not reference a UI framework at all.

## 2. The list that updates itself (20 min)

```csharp
public sealed class CatalogueViewModel : ICatalogueViewModel
{
    private readonly ObservableCollection<IItem> _shown = [];

    public CatalogueViewModel(IItemStore store)
    {
        _dex = (YourStore)store;
        _dex.ItemAdded += OnItemAdded;     // Lab 06, collecting its debt
        Refill();
    }

    public IEnumerable<IItem> Items => _shown;   // the same object, every time
}
```

Three rules, and each is a check:

1. **`ObservableCollection<T>`, not `List<T>`.** A `List` looks identical right up until something is added and the window just sits there. `ObservableCollection` tells the window.
2. **The same instance, always.** A binding grabs the collection once and holds it. Hand back a new one — from `ToList()`, or by reassigning on every filter change — and the window goes on showing the first one forever.
3. **Add one item, not all of them.** When an entry arrives, `_shown.Add(item)`. Clearing and refilling announces a *Reset*, which tells the window to throw away every row and redraw — that's the flicker, the lost scroll position and the lost selection, all from one shortcut.

The subscription is Lab 06's event finally earning its keep: the store announces, the view model adds, the window redraws. Nobody calls "refresh".

## 3. Telling the window (15 min)

`INotifyPropertyChanged` is one event, and binding is the window listening to it. A property that changes quietly is a label that never updates.

```csharp
public string Filter
{
    get => _filter;
    set
    {
        var wanted = value ?? "";        // a binding hands you null the moment the box is cleared
        if (wanted == _filter) return;   // nothing changed, so nobody needs telling
        _filter = wanted;
        Announce();                      // [CallerMemberName] fills in "Filter"
        Refill();
    }
}
```

Two things people miss, both checked:

- **A property computed from another one has to be announced too.** `Details` changes when `Selected` changes, so setting `Selected` announces both.
- **Setting a property to the value it already has should announce nothing.** In a real window this is the difference between redrawing when something changed and redrawing on every keystroke.

## 4. Buttons that grey themselves out (15 min)

A command is two questions the window keeps asking — *can I?* and, when clicked, *do it* — plus an event telling the button to ask again. Ten lines:

```csharp
public sealed class RelayCommand(Action<object?> run, Func<object?, bool>? can = null) : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? p) => can?.Invoke(p) ?? true;
    public void Execute(object? p) { if (CanExecute(p)) run(p); }
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
```

`ICommand` lives in `System.ObjectModel`, not in WPF or Avalonia — which is why none of this needs a UI framework.

- `RemoveCommand.CanExecute` is false with nothing selected. That's what greys the button out.
- When the selection changes, **raise `CanExecuteChanged`** — the button only re-asks when it is told to, and without this it stays greyed out with a row selected. It is the single most common bug in this lab.
- Expose the commands as properties set **once**, in the constructor. A property that news up a command on every read hands the button an object nobody will ever raise an event on.

## 5. The window (20 min)

Now the easy part. A search box, a list, a details panel, a Delete button, a count. Every attribute names something on the view model:

```xml
<TextBox   Text="{Binding Filter, Mode=TwoWay}" />
<ListBox   ItemsSource="{Binding Items}" SelectedItem="{Binding Selected, Mode=TwoWay}" />
<TextBlock Text="{Binding Details}" />
<Button    Content="Delete" Command="{Binding RemoveCommand}" />
```

**Your code-behind stays empty.** That's the test you can run without a test runner: if there's logic in `MainWindow.axaml.cs`, it's logic no test can reach.

Set `DataContext` once, where the window is created. The reference does it in `App.axaml.cs`, and loads the Lab 08 file first — so the window opens showing what you saved last time.

> **Getting started:** WPF — *New Project → WPF Application*, or `dotnet new wpf`. Avalonia — `dotnet new install Avalonia.Templates` then `dotnet new avalonia.app`. On a laptop with the .NET 10 SDK, add `-f net9.0` to `dotnet new` (or change `net10.0` to `net9.0` in the new project file afterwards; for WPF it's `net9.0-windows`): the lab PCs have .NET 9. Put the versions you install in `Directory.Packages.local.props`; that file is yours and `sq update` never touches it. The reference has a working copy of both the project file and the XAML.

## 6. Push (5 min)

```
sq test
git add . && git commit -m "Lab 09: a window" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 09 at full marks, Labs 02–08 still green, CI green, and a window you can show me.

**Your UI project is not part of the graded build.** `sq test` and CI build the contract tests and your own tests by name — so a half-finished window, or a WPF project that can't compile on Linux, can never turn another lab red. Break it freely.

## Stretch

**Let the window save.** Add a `SaveCommand` that calls your Lab 08 `SaveAsync` — and notice the problem immediately: saving is `async`, commands are `void`. Find out what `async void` does to an exception (it doesn't go where you expect), and why every MVVM framework ships an `AsyncRelayCommand` that disables itself while it runs.

Or: **sort the list by clicking a column header.** One more property, one more command, and the same three rules.

---

**Next session, S10:** a web API over the same model. The same store, a third front end, and the first time your capstone is reachable by something that isn't you.
