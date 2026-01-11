namespace Koncar.Interview.Client.Presentation.Tui.Views.Characters;

using Koncar.Interview.Client.Application.Contracts.Models;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;
using System;
using Terminal.Gui.Drivers;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

internal sealed class CreateCharacterView : Dialog, ICreateCharacterView
{
    private readonly Label _nameLabel;
    private readonly TextField _nameField;
    private readonly Label _descriptionLabel;
    private readonly TextField _descriptionField;
    private readonly Button _saveButton;
    private readonly Button _cancelButton;

    public event EventHandler<CreateCharacterModel> CreateRequested = default!;

    public CreateCharacterView()
    {
        Title = "Create character";
        Y = Pos.Center();
        X = Pos.Center();

        _nameLabel = new()
        {
            Y = 3,
            X = Pos.Center(),
            Title = "Name"
        };

        _nameField = new()
        {
            Y = Pos.Bottom(_nameLabel),
            X = Pos.Center(),
            Width = 40,
            CursorVisibility = CursorVisibility.Underline,
        };

        _descriptionLabel = new()
        {
            Y = Pos.Bottom(_nameField) + 1,
            X = Pos.Center(),
            Title = "Description",
        };

        _descriptionField = new()
        {
            Y = Pos.Bottom(_descriptionLabel),
            X = Pos.Center(),
            Width = 40,
            CursorVisibility = CursorVisibility.Underline,
        };

        _saveButton = new()
        {
            Title = "Save",
            Y = Pos.Bottom(_descriptionField) + 1,
            X = Pos.Left(_descriptionField)
        };

        _cancelButton = new()
        {
            Title = "Cancel",
            X = Pos.Right(_saveButton) + 2,
            Y = Pos.Bottom(_descriptionField) + 1
        };

        _saveButton.Activating += (_, _) => OnCreateRequested();
        _cancelButton.Activating += (_, _) => OnCancelRequested();

        Add(
            _nameLabel,
            _nameField,
            _descriptionLabel,
            _descriptionField);

        AddButton(_cancelButton);
        AddButton(_saveButton);
    }


    private void OnCreateRequested()
    {
        CreateCharacterModel model = new(
            _nameField.Text ?? string.Empty,
            _descriptionField.Text);

        CreateRequested.Invoke(this, model);
        
        ResetForm();
        RequestStop();
    }

    private void OnCancelRequested()
    {
        ResetForm();
        RequestStop();
    }

    private void ResetForm()
    {
        _nameField.Text = string.Empty;
        _descriptionField.Text = string.Empty;
    }
}
