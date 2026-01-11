namespace Koncar.Interview.Client.Presentation.Tui.Windows;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Views;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Windows;
using Koncar.Interview.Client.Presentation.Tui.Views;
using Koncar.Interview.Client.Presentation.Tui.Views.Characters;
using System;
using Terminal.Gui.Input;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

public sealed class MainWindow : Window, IMainWindow
{
    private static readonly Key Cancel = new Key(Key.C).WithCtrl;
    
    public event EventHandler RefreshRequested = default!;
    public event EventHandler CancellationRequested = default!;

    public MainWindow()
    {
        Width = Dim.Fill();
        Height = Dim.Fill();
        KeyUp += (s, e) => OnKeyUp(e);
        CharacterListView = new CharacterListView();
        CharacterDetailView = new CharacterDetailView(Pos.Right((View)CharacterListView));
        MainMenu = new MainMenu();

        Add(
            (View)MainMenu,
            (View)CharacterListView,
            (View)CharacterDetailView);
    }

    public ICharacterListView CharacterListView { get; }
    public ICharacterDetailView CharacterDetailView { get; }
    public IMainMenu MainMenu { get; }

    public void ShowError(string message)
    {
        App?.Invoke(() => MessageBox.ErrorQuery(App, "An error occurred", message, "OK"));
    }

    private new void OnKeyUp(Key key)
    {
        if (key == Key.F5)
        {
            RefreshRequested?.Invoke(this, EventArgs.Empty);
        }

        if (key == Cancel)
        {
            CancellationRequested.Invoke(this, EventArgs.Empty);
            key.Handled = true;
        }

    }
}
