namespace Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;

using Koncar.Interview.Client.Domain.Entities;
using System;

public interface ICharacterDetailView : ILoadableView, IHideableView
{
    public event EventHandler<Character> CharacterUpdate;
    public event EventHandler<long> CharacterDelete;

    public void SetSource(Character source);
}
