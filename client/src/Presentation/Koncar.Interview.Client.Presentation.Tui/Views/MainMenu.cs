namespace Koncar.Interview.Client.Presentation.Tui.Views;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Views;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;
using Koncar.Interview.Client.Presentation.Tui.Views.Characters;
using Terminal.Gui.App;
using Terminal.Gui.Views;

internal sealed class MainMenu : MenuBar, IMainMenu
{
    public MainMenu()
    {
        Menus = SetupMenu();

        CreateCharacterDialog = new CreateCharacterView();
    }

    public ICreateCharacterView CreateCharacterDialog { get; }

    private MenuBarItem[] SetupMenu()
    {
        return [
            new MenuBarItem(
                "_File",
                [
                    new MenuItem(
                        "_Create",
                        "Create new character",
                        OnCreateRequested),
                    new MenuItem(
                        "_Exit",
                        "Quit the application",
                        OnExitRequested)
                ])
            ];
    }

    private void OnExitRequested()
    {
        int? result = MessageBox.Query(
            App,
            "Exit",
            "Do you really want to quit",
            "Yes",
            "No");

        if (result is not null && result == 0)
        {
            App?.RequestStop();
        }
    }

    private void OnCreateRequested()
    {
        App?.Invoke(() => App.Run((IRunnable)CreateCharacterDialog));
    }
}
