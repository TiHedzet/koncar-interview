namespace Koncar.Interview.Client.Presentation.Tui.Presenters;

using ErrorOr;
using Koncar.Interview.Client.Application.Characters.Commands;
using Koncar.Interview.Client.Application.Characters.Queries;
using Koncar.Interview.Client.Application.Contracts.Models;
using Koncar.Interview.Client.Domain.Entities;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Presenters;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Windows;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

internal sealed class CharacterPresenter : PresenterBase, ICharacterPresenter
{
    private readonly IMainWindow _mainWindow;
    private readonly IMediator _mediator;

    public CharacterPresenter(
        ILogger<CharacterPresenter> logger,
        IMainWindow mainWindow,
        IMediator mediator)
        : base(logger)
    {
        _mainWindow = mainWindow;
        _mediator = mediator;

        MapHandlers();
    }

    protected override void MapHandlers()
    {
        _mainWindow.CharacterListView.Initialized +=
            async (_, _) => await HandleEvent(
                LoadCharactersAsync,
                _mainWindow.CharacterListView);

        _mainWindow.CharacterListView.RowSelected +=
            async (_, id) => await HandleEvent(
                GetCharacterAsync,
                id,
                _mainWindow.CharacterListView);

        _mainWindow.RefreshRequested +=
            async (_, _) => await HandleEvent(
                LoadCharactersAsync,
                _mainWindow.CharacterListView);

        _mainWindow.CancellationRequested += (_, _) => CancelOperation();

        _mainWindow.CharacterDetailView.CharacterUpdate +=
            async (_, character) => await HandleEvent(
                UpdateCharacterAsync,
                character,
                _mainWindow.CharacterDetailView);

        _mainWindow.CharacterDetailView.CharacterDelete +=
            async (_, id) => await HandleEvent(
                DeleteCharacterAsync,
                id,
                _mainWindow.CharacterDetailView);

        _mainWindow.MainMenu.CreateCharacterDialog.CreateRequested +=
            async (_, model) => await HandleEvent(
                CreateCharacterAsync,
                model);
    }

    private async Task LoadCharactersAsync(CancellationToken cancellationToken)
    {
        _mainWindow.CharacterListView.Hide();

        ErrorOr<List<Character>> result = await _mediator.Send(
            new GetCharacters.Query(),
            cancellationToken);

        if (result.IsError)
        {
            _mainWindow.ShowError(result.FirstError.Description);
            return;
        }

        _mainWindow.CharacterListView.SetSource(result.Value);
        _mainWindow.CharacterListView.Show();
    }

    private async Task GetCharacterAsync(long id, CancellationToken cancellationToken)
    {
        ErrorOr<Character> result = await _mediator.Send(new GetCharacter.Query(id), cancellationToken);

        if (result.IsError)
        {
            _mainWindow.ShowError(result.FirstError.Description);
            return;
        }

        _mainWindow.CharacterDetailView.SetSource(result.Value);
        _mainWindow.CharacterDetailView.Show();
        _mainWindow.CharacterListView.Show();
    }

    private async Task UpdateCharacterAsync(
        Character character,
        CancellationToken cancellationToken)
    {
        _mainWindow.CharacterDetailView.Hide();

        UpdateCharacter.Command command = new(character);
        ErrorOr<Character> result = await _mediator.Send(
            command,
            cancellationToken);

        if (result.IsError)
        {
            _mainWindow.ShowError(result.FirstError.Description);
            return;
        }

        _mainWindow.CharacterDetailView.SetSource(result.Value);
        _mainWindow.CharacterDetailView.Show();

        await LoadCharactersAsync(cancellationToken);
    }

    private async Task DeleteCharacterAsync(
        long id,
        CancellationToken cancellationToken)
    {
        _mainWindow.CharacterDetailView.Hide();

        DeleteCharacter.Command command = new(id);

        ErrorOr<Success> result = await _mediator.Send(
            command,
            cancellationToken);

        if (result.IsError)
        {
            _mainWindow.ShowError(result.FirstError.Description);
            return;
        }

        await LoadCharactersAsync(cancellationToken);
    }

    private async Task CreateCharacterAsync(
        CreateCharacterModel model,
        CancellationToken cancellationToken)
    {
        CreateCharacter.Command command = new(
            model.Name,
            model.Description);

        ErrorOr<Character> result = await _mediator.Send(command, cancellationToken);

        if (result.IsError)
        {
            _mainWindow.ShowError(result.FirstError.Description);
            return;
        }

        _mainWindow.CharacterDetailView.SetSource(result.Value);
        _mainWindow.CharacterDetailView.Show();
        await LoadCharactersAsync(cancellationToken);
    }

    private void CancelOperation()
    {
        _cts.Cancel();
    }
}
