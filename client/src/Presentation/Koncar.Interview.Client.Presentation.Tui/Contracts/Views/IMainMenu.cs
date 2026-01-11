namespace Koncar.Interview.Client.Presentation.Tui.Contracts.Views;

using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;

public interface IMainMenu
{
    public ICreateCharacterView CreateCharacterDialog { get; }
}
