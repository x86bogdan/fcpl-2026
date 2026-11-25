# Lab 09 — A window

**Today:** your capstone gets a face. The same model and the same store, now in a window you can click, built with **Avalonia**, plus the idea that makes a user interface testable at all.

**Required feature (matrix row 9):** a working window bound to your model (WPF or Avalonia)

> **In your project:** open your theme's spec ([the 21 themes](https://github.com/x86bogdan/fcpl-2026/blob/main/themes/README.md)) → *What each session adds* → row **9** (your own idea: the required feature above, said for your app). Before you write code, put three lines in your README: which of your classes change today, what they do in your app, and where you'll see it working.

---

## What you build today

Today has more new ideas than any lab so far: a UI framework, XAML, project templates, NuGet packages, MVVM, and a second project next to `Capstone.Core`. Section 1 explains why each one is there. The table is the to-do list, in order.

| # | You build | Where | What it is | Brief | Checks |
|---|---|---|---|---|---|
| 1 | an **Avalonia project** that opens a window | `src/Capstone.Ui/` | made by a template, fixed in three places | §2 | — |
| 2 | `Lab09Wiring` | `Capstone.Core/Wiring/` | `CreateStore`, `CreateSample` (as in Lab 02), `CreateViewModel(store)` (one line) | §4 | — |
| 3 | a **view model** implementing `ICatalogueViewModel` | `Capstone.Core/ViewModels/` | everything the window shows, in a class that has never heard of a Button | §4–§6 | all 43 |
| | ↳ `Items`, `Count` | | the list the window shows, and how many are showing | §4 | The list (11) |
| | ↳ `Filter`, `Selected`, `Details` | | the search text, the row picked, one line about it | §5 | The search box (9) · Telling the window (9) |
| | ↳ `AddCommand`, `RemoveCommand` + `RelayCommand` | | the two buttons, and ten lines that make a command | §6 | The buttons (11) |
| 4 | **the window**, bound to your view model | `Capstone.Ui/MainWindow.axaml` | a search box, a list, a details panel, Delete, a count | §3, §7 | **none**: you show it to me |
| 5 | whatever your theme needs on screen | `Capstone.Ui/` | a form for Add, pictures, a second list… | §8 | — |

For rows 2 and 3, let the IDE write the members: create `public sealed class Lab09Wiring : ILab09Wiring { }` and your view model class, then **Alt+Enter → Implement missing members** (Rider) or **Ctrl+. → Implement interface** (Visual Studio, VS Code). Lab 09 turns from *not started* to red, and each member you fill in turns its checks green.

**The tests grade the view model. Your theme decides what the window shows**: which items, what `Details` says about one, which buttons you add. That's your three README lines, and the window is what you show me.

## 0. Start

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

**Finishing early?** The stretch task is at the bottom of this page, worth up to 2 extra points when the lab is on time.

## 1. Why a window needs a second project

**A UI framework** is a library that draws windows, buttons and lists, and tells your code when someone clicks. .NET has several. We use **Avalonia**: it runs on Windows, Linux and macOS, so it works on the lab PCs and on every laptop. **WPF**, which you may meet in jobs, is its Windows-only ancestor: almost the same XAML and the same ideas.

**XAML** describes a window instead of building it in code. You write *what* is on screen, and the framework creates the objects:

```xml
<!-- XAML: what the window contains -->
<Button Content="Delete" Margin="8" />
```

```csharp
// the same thing as C#, which you will not write
var button = new Button { Content = "Delete", Margin = new Thickness(8) };
```

**MVVM** (Model, View, ViewModel) is how XAML applications are organised, and you already have two thirds of it:

| | What it is | In your capstone | Project |
|---|---|---|---|
| **Model** | the data and its rules | your items and your store, Labs 02–08 | `Capstone.Core` |
| **ViewModel** | what the screen shows, as plain C#: the list, the search text, the selection, the commands | **today's class** | `Capstone.Core` |
| **View** | the window: XAML and bindings, nothing else | **today's window** | `Capstone.Ui` |

> **Everything about what the screen shows lives in a class that has never heard of a Button.**

**Why the window lives in its own project.** `Capstone.Core` is your application's brain: the console app uses it, today's window uses it, and next session's web API will use it. If Core referenced Avalonia, all three would drag a UI framework along. So the arrow points one way: `Capstone.Ui` references `Capstone.Core`, never the reverse. The tests check that too: your domain library must not reference a UI framework at all.

**Why the tests can't see your window.** The checks run on a build server in the cloud, on Linux, with no screen. So they test the view model, which is ordinary C#, and drive your whole interface without a window existing. Your window you show me in the room.

| Part | Where it lives | How it's graded |
|---|---|---|
| The view model | `src/Capstone.Core/ViewModels/` | 43 automatic checks |
| The window | `src/Capstone.Ui/` | you show it to me, in the room |

**Your UI project is not part of the graded build.** `sq test` and CI build the contract tests and your own tests by name, so a half-finished window can never turn another lab red. Break it freely.

## 2. Create the Avalonia project

Two new tools first.

- **A project template** is a ready-made starting project. `dotnet new console` used one you already had. Avalonia's templates are downloaded once, then `dotnet new avalonia.app` creates a project from them.
- **A NuGet package** is a library downloaded from [nuget.org](https://www.nuget.org/). Avalonia itself is five of them. Your repository keeps every package's version in one place (`Directory.Packages.props`, *central package management*), so a project file names a package but never its version. Your own packages go in **`Directory.Packages.local.props`**: that file is yours, and `sq update` never touches it.

Do this first, before anything else: the downloads are faster while the network is quiet. In a terminal at your repository's root (on a lab PC: **Rider's terminal**):

**① Install the templates** (once per computer):

```
dotnet new install Avalonia.Templates
```

**② Create the project.** Use **Avalonia .NET App** (`avalonia.app`). Not *Avalonia .NET MVVM App*: that one puts the view model in the UI project, where the tests can't find it.

```
dotnet new avalonia.app -n Capstone.Ui -o src/Capstone.Ui
```

Don't add `-f net9.0`: the template only offers `net10.0` and `net11.0`, and step ③ takes care of it. The project dialogs in Visual Studio and Rider sometimes show only the F# templates; the command above always works.

**③ Fix three things.** Opening the new project right now fails with **NU1008** (a version where central package management expects none). In `src/Capstone.Ui/Capstone.Ui.csproj`:

- delete the `<TargetFramework>…</TargetFramework>` line: the project then builds for `net9.0`, like the rest of your repository and the lab PCs
- delete every `Version="…"` from the `<PackageReference>` lines

…and create `Directory.Packages.local.props` at the root of your repository, next to `Directory.Packages.props`, with the versions you just deleted:

```xml
<Project>
  <ItemGroup>
    <PackageVersion Include="Avalonia" Version="12.2.0" />
    <PackageVersion Include="Avalonia.Desktop" Version="12.2.0" />
    <PackageVersion Include="Avalonia.Themes.Fluent2" Version="12.2.0" />
    <PackageVersion Include="Avalonia.Fonts.Inter" Version="12.2.0" />
    <PackageVersion Include="AvaloniaUI.DiagnosticsSupport" Version="2.2.3" />
  </ItemGroup>
</Project>
```

If your template wrote other numbers, use those. They only have to match each other.

**④ Connect it to Core and to the solution:**

```
dotnet add src/Capstone.Ui reference src/Capstone.Core
dotnet sln SharpQuest.slnx add src/Capstone.Ui
```

**⑤ Run it:**

```
dotnet run --project src/Capstone.Ui
```

A window that says *Welcome to Avalonia!* means everything works. Close it. The first build downloads the packages and takes a minute.

**What the template made:**

| File | What it does | You change it? |
|---|---|---|
| `Capstone.Ui.csproj` | the project: an executable, the Avalonia packages, a reference to Core | step ③ only |
| `Program.cs` | `Main`: starts Avalonia | no |
| `App.axaml` | the whole application: the theme (`<Fluent2Theme />`) | no |
| `App.axaml.cs` | creates the main window when the app starts | **yes, one line** (§7) |
| `MainWindow.axaml` | the window, in XAML | **yes**: today's work |
| `MainWindow.axaml.cs` | the window's code-behind | **no**: it stays empty |

Avalonia's XAML files end in **`.axaml`**; WPF's end in `.xaml`. Same language.

## 3. XAML: describe a window

A window is a tree of **controls**. A few are enough for today:

| Kind | Controls | For |
|---|---|---|
| Layout | `Grid`, `StackPanel`, `DockPanel` | where things go: rows and columns, a pile, edges |
| Input | `TextBox`, `Button`, `CheckBox`, `ComboBox` | what the user types, clicks and picks |
| Display | `TextBlock`, `ListBox`, `Image` | text, a list of items, a picture |

```xml
<Grid RowDefinitions="Auto,*,Auto" ColumnDefinitions="2*,3*" Margin="12">
  <TextBox   Grid.Row="0" Grid.Column="0" PlaceholderText="Search" />
  <Button    Grid.Row="0" Grid.Column="1" Content="Delete" HorizontalAlignment="Right" />
  <ListBox   Grid.Row="1" Grid.Column="0" />
  <TextBlock Grid.Row="1" Grid.Column="1" Text="Nothing selected" />
  <TextBlock Grid.Row="2" Grid.Column="0" Text="0 showing" />
</Grid>
```

`Auto` is as tall as its content, `*` takes what's left, `2*,3*` splits the width two to three. Attributes are properties: `Margin`, `HorizontalAlignment`, `Text`.

**Try it before your project even builds:** paste that into the [**Avalonia XAML Playground**](https://avaloniaui.github.io/XamlPlayground/) and the window appears in your browser, no install. It's the fastest way to try a layout or a control you have never used. Every control, with examples: [docs.avaloniaui.net](https://docs.avaloniaui.net/).

**Binding** is how a control gets its value from your view model instead of a fixed string:

```xml
<TextBlock Text="{Binding Details}" />
```

means *"show the `Details` property of whatever object this window is looking at, and update when it announces a change"*. That object is the window's `DataContext`: your view model (§7). Sections 4–6 build the properties a binding can listen to.

## 4. The view model: a list that updates itself

**Where it goes.** The view model is a class of your own in `src/Capstone.Core/ViewModels/` that implements the contract's `ICatalogueViewModel`. The tests reach it through `ILab09Wiring`, which you implement in `src/Capstone.Core/Wiring/` in a new `Lab09Wiring` class: `CreateStore` and `CreateSample` are the ones from your Lab 02 wiring, and `CreateViewModel(store)` is one line, `new CatalogueViewModel(store)` with your class's name.

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
2. **The same instance, always.** A binding grabs the collection once and holds it. Hand back a new one, from `ToList()` or by reassigning on every filter change, and the window goes on showing the first one forever.
3. **Add one item, not all of them.** When an entry arrives, `_shown.Add(item)`. Clearing and refilling announces a *Reset*, which tells the window to throw away every row and redraw: the flicker, the lost scroll position and the lost selection, all from one shortcut.

The subscription is Lab 06's event finally earning its keep: the store announces, the view model adds, the window redraws. Nobody calls "refresh".

## 5. Telling the window

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

## 6. Buttons that grey themselves out

A command is two questions the window keeps asking (*can I?* and, when clicked, *do it*) plus an event telling the button to ask again. Ten lines, in `Capstone.Core`:

```csharp
public sealed class RelayCommand(Action<object?> run, Func<object?, bool>? can = null) : ICommand
{
    public event EventHandler? CanExecuteChanged;
    public bool CanExecute(object? p) => can?.Invoke(p) ?? true;
    public void Execute(object? p) { if (CanExecute(p)) run(p); }
    public void RaiseCanExecuteChanged() => CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}
```

`ICommand` lives in `System.ObjectModel`, not in Avalonia, which is why none of this needs a UI framework.

- `RemoveCommand.CanExecute` is false with nothing selected. That's what greys the button out.
- When the selection changes, **raise `CanExecuteChanged`**: the button only re-asks when it is told to, and without this it stays greyed out with a row selected. It is the single most common bug in this lab.
- `AddCommand`'s parameter is the item to add: the tests hand it one, and your window would build it from a form. It calls your **store's** `Add`, not `_shown.Add`. The store's rules still apply, and the store's event is what puts the item in the list (§4), so it never appears twice.
- Expose the commands as properties set **once**, in the constructor. A property that news up a command on every read hands the button an object nobody will ever raise an event on.

*Seen `[ObservableProperty]` and `[RelayCommand]` online?* That's the CommunityToolkit.Mvvm package, which writes this code for you. Write it by hand today: the questions you'll be asked are about the code it would hide.

## 7. Bind the window to your view model

**Give the window its view model, once.** In `App.axaml.cs`, where the template creates the window (and `using Capstone.Core.Wiring;` at the top):

```csharp
var wiring = new Lab09Wiring();
var store = wiring.CreateStore();
for (var i = 1; i <= 5; i++) store.Add(wiring.CreateSample(i));   // or load your Lab 08 file

desktop.MainWindow = new MainWindow { DataContext = wiring.CreateViewModel(store) };
```

**Tell the XAML what it's bound to.** Avalonia checks bindings when it builds (*compiled bindings*), so the window has to say which class it's looking at, and a list's rows which class they show. Add two namespaces and `x:DataType` to `MainWindow.axaml`:

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:vm="using:Capstone.Core.ViewModels"
        xmlns:c="using:SharpQuest.Contracts"
        x:Class="Capstone.Ui.MainWindow"
        x:DataType="vm:CatalogueViewModel"
        Title="My capstone">

  <Grid RowDefinitions="Auto,*,Auto" ColumnDefinitions="2*,3*" Margin="12" ColumnSpacing="12" RowSpacing="8">
    <TextBox Grid.Row="0" Grid.Column="0" PlaceholderText="Search"
             Text="{Binding Filter, Mode=TwoWay}" />
    <Button  Grid.Row="0" Grid.Column="1" HorizontalAlignment="Right"
             Content="Delete" Command="{Binding RemoveCommand}" />
    <ListBox Grid.Row="1" Grid.Column="0"
             ItemsSource="{Binding Items}" SelectedItem="{Binding Selected, Mode=TwoWay}">
      <ListBox.ItemTemplate>
        <DataTemplate x:DataType="c:IItem">
          <TextBlock Text="{Binding DisplayName}" />
        </DataTemplate>
      </ListBox.ItemTemplate>
    </ListBox>
    <TextBlock Grid.Row="1" Grid.Column="1" Text="{Binding Details}" TextWrapping="Wrap" />
    <TextBlock Grid.Row="2" Grid.Column="0" Grid.ColumnSpan="2"
               Text="{Binding Count, StringFormat='{}{0} showing'}" />
  </Grid>
</Window>
```

Use your own view model's class name in `x:DataType`. A misspelt property name in a binding is now a **build error** (AVLN2000), not a silently empty window, which is the best thing about compiled bindings.

`Mode=TwoWay` sends changes both ways: typing changes `Filter`, picking a row changes `Selected`. The `DataTemplate` says how one row looks, so a row can be more than a name (§8).

**Your code-behind stays empty.** That's the test you can run without a test runner: if there's logic in `MainWindow.axaml.cs`, it's logic no test can reach.

Run it: type in the search box, pick a row, delete it. The reference solution, released after the lab closes, has a complete working copy that loads the Lab 08 file instead of samples.

## 8. Make it yours

The five controls above are the minimum. Your theme decides the rest, so look at your row 9 and pick what your app needs. Try each idea in the Playground first.

- **Add something from a form.** A few `TextBox`es and an Add button. `AddCommand` takes the new item as its parameter, so give the view model the form's fields as properties and build the item from them, or bind `CommandParameter`.
- **A row that shows more.** The `DataTemplate` can hold a `StackPanel` with two `TextBlock`s, or an `Image` and a name.
- **Pick a kind.** A `ComboBox` bound to a list of kinds (your Lab 04 types), and the list follows it.
- **Look.** `RequestedThemeVariant="Dark"` in `App.axaml`, a `Border` with `CornerRadius`, `FontSize`, `Spacing` on a `StackPanel`.

Anything you add to the window stays outside the tests. Anything you add to the view model can be tested by your own tests.

## 9. Push

```
sq test
git add . && git commit -m "Lab 09: a window" && git push
```

**No git (lab PC)?** `sh sq.sh test`, then `sh sq.sh upload`, then **Add file → Upload files** on your repository's page: drag in what it gathered, **Commit changes**. Step by step: [working on a lab PC](https://github.com/x86bogdan/fcpl-2026/blob/main/LAB-PC.md).

**On time** means green in your repository's **Actions** tab by **Friday 23:59** this week, whichever group you're in. After that it's late work, worth up to 8.

## Done means

See `ACCEPTANCE.md`. In short: Lab 09 at full marks, Labs 02–08 still green, CI green, and a window you can show me.

## When it doesn't work

| You see | It means |
|---|---|
| `'net9.0' is not a valid value for -f` | leave `-f` out; step ③ sets the framework |
| **NU1008** … *cannot define a value for Version* | step ③: versions belong in `Directory.Packages.local.props`, not in the `.csproj` |
| **NU1010** … *do not define a corresponding PackageVersion item* | a package in the `.csproj` has no line in `Directory.Packages.local.props` |
| an error about `net11.0` or `net10.0` on a lab PC | the `<TargetFramework>` line is still in `Capstone.Ui.csproj` |
| **AVLN2000** *Unable to resolve property or method of name …* | a misspelt name in a `{Binding}`, or `x:DataType` names the wrong class |
| **AVLN2100** *… without an explicit x:DataType directive* | `x:DataType` missing on the `<Window>`, or on the `DataTemplate` |
| **AVLN3000** *… multiple assignments to the property Content* | the template's *Welcome to Avalonia!* is still inside `<Window>` next to your `Grid`: a window holds one thing |
| the window opens but stays empty | `DataContext` never set, or set twice (once in XAML, once in code) |
| the list never grows | `List<T>` instead of `ObservableCollection<T>`, or a new collection each time (§4) |
| Delete stays grey with a row selected | `RaiseCanExecuteChanged` missing (§6) |
| `references Avalonia` in the MVVM checks | a UI type got into `Capstone.Core`, or the view model is in `Capstone.Ui` |

## Stretch

**Let the window save.** Add a `SaveCommand` that calls your Lab 08 `SaveAsync`, and notice the problem immediately: saving is `async`, commands are `void`. Find out what `async void` does to an exception (it doesn't go where you expect), and why every MVVM framework ships an `AsyncRelayCommand` that disables itself while it runs.

Or: **sort the list by clicking a column header.** One more property, one more command, and the same three rules.

---

**Next session, S10:** a web API over the same model. The same store, a third front end, and the first time your capstone is reachable by something that isn't you.
