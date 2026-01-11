namespace Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;

using Koncar.Interview.Client.Domain.Entities;
using System;
using System.Collections.Generic;

public interface ICharacterListView : ILoadableView, IHideableView
{
    public event EventHandler<long> RowSelected;
    public event EventHandler Initialized;

    public void SetSource(IEnumerable<Character> source);

}
