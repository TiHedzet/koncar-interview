namespace Koncar.Interview.Client.Presentation.Tui.Contracts.Windows;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Views;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;

public interface IMainWindow
{
    public event EventHandler RefreshRequested;
    public event EventHandler CancellationRequested;

    public ICharacterDetailView CharacterDetailView { get; }
    public ICharacterListView CharacterListView { get; }
    public IMainMenu MainMenu { get; }

    public void ShowError(string message);
}
