namespace Koncar.Interview.Client.Presentation.Tui.Views.Characters;

using Koncar.Interview.Client.Domain.Entities;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

internal sealed class CharacterListView : FrameView, ICharacterListView
{
    private readonly ListView _characters;
    private readonly Label _loading;

    public event EventHandler<long> RowSelected = default!;
    public new event EventHandler Initialized = default!;

    public CharacterListView()
    {
        Width = Dim.Percent(50);
        Height = Dim.Fill();
        Y = 1;

        _characters = new ListView()
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
        };


        _characters.SelectedItemChanged += (_, args) => OnRowSelected(args);
        _characters.Initialized += (_, args) => OnInitialized();

        _loading = new Label()
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Text = "Loading...",
            Visible = false,
        };

        Add(_characters, _loading);
    }

    public void LoadingStarted()
    {
        Hide();
        _loading.Visible = true;
    }
    public void LoadingFinished()
    {
        _loading?.Visible = false;
    }

    private void OnRowSelected(ListViewItemEventArgs args)
    {
        string? rowIdentifier = (args.Value as string)?
            .Split(' ', 2)
            .FirstOrDefault();

        if (string.IsNullOrEmpty(rowIdentifier))
        {
            return;
        }

        if (long.TryParse(rowIdentifier, out long id))
        {
            RowSelected.Invoke(this, id);
        }
    }

    private void OnInitialized()
    {
        Initialized?.Invoke(this, EventArgs.Empty);
    }

    public void Hide()
    {
        _characters.Visible = false;
    }

    public void Show()
    {
        _characters.Visible = true;
    }

    public void SetSource(IEnumerable<Character> entities)
    {
        _characters.SetSource(new ObservableCollection<string>(entities.Select(c => $"{c.Id} - {c.Name}")));
    }
}
