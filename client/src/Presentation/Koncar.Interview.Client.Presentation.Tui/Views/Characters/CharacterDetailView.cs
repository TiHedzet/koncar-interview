namespace Koncar.Interview.Client.Presentation.Tui.Views.Characters;

using Koncar.Interview.Client.Domain.Entities;
using Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;
using System;
using Terminal.Gui.ViewBase;
using Terminal.Gui.Views;

internal sealed class CharacterDetailView : FrameView, ICharacterDetailView
{
    private readonly Label _idFieldLabel;
    private readonly TextField _idField;
    private readonly Label _nameFieldLabel;
    private readonly TextField _nameField;
    private readonly Label _descriptionFieldLabel;
    private readonly TextField _descriptionField;
    private readonly Button _saveButton;
    private readonly Button _deleteButton;
    private readonly Label _loading;

    private Character? _character = default;

    public event EventHandler<Character> CharacterUpdate = default!;
    public event EventHandler<long> CharacterDelete = default!;

    public CharacterDetailView(Pos columnPosition)
    {
        X = columnPosition;
        Y = 1;
        Width = Dim.Percent(50);
        Height = Dim.Fill();

        _idFieldLabel = new Label()
        {
            Title = "Id",
            Visible = false
        };

        _idField = new TextField()
        {
            Y = Pos.Bottom(_idFieldLabel),
            X = 0,
            Width = 40,
            ReadOnly = true,
            Visible = false
        };

        _nameFieldLabel = new Label()
        {
            Y = Pos.Bottom(_idField),
            Title = "Name",
            Visible = false
        };

        _nameField = new TextField()
        {
            Y = Pos.Bottom(_nameFieldLabel),
            Width = 40,
            Visible = false
        };

        _descriptionFieldLabel = new Label()
        {
            Y = Pos.Bottom(_nameField),
            Title = "Description",
            Visible = false
        };

        _descriptionField = new TextField()
        {
            Y = Pos.Bottom(_descriptionFieldLabel),
            Width = 40,
            Visible = false
        };

        _saveButton = new Button()
        {
            Y = Pos.Bottom(_descriptionField),
            Title = "Save",
            Visible = false
        };

        _deleteButton = new Button()
        {
            Y = Pos.Bottom(_descriptionField),
            X = Pos.Right(_saveButton) + 10,
            Title = "Delete",
            Visible = false
        };

        _loading = new Label()
        {
            X = Pos.Center(),
            Y = Pos.Center(),
            Title = "Loading ...",
            Visible = false
        };

        _saveButton.Activating += (_, _) => OnCharacterUpdate();
        _deleteButton.Activating += (_, _) => OnCharacterDelete();

        Add(
            _idFieldLabel,
            _idField,
            _nameFieldLabel,
            _nameField,
            _descriptionFieldLabel,
            _descriptionField,
            _saveButton,
            _deleteButton,
            _loading);
    }

    public void Hide()
    {
        _idFieldLabel.Visible = false;
        _idField.Visible = false;
        _nameFieldLabel.Visible = false;
        _nameField.Visible = false;
        _descriptionFieldLabel.Visible = false;
        _descriptionField.Visible = false;
        _saveButton.Visible = false;
        _deleteButton.Visible = false;
    }

    public void LoadingFinished()
    {
        _loading.Visible = false;
    }

    public void LoadingStarted()
    {
        _loading.Visible = true;
    }

    public void Show()
    {
        _idField.Text = $"{_character?.Id}";
        _nameField.Text = _character?.Name ?? string.Empty;
        _descriptionField.Text = _character?.Description ?? string.Empty;

        _idFieldLabel.Visible = true;
        _idField.Visible = true;
        _nameFieldLabel.Visible = true;
        _nameField.Visible = true;
        _descriptionFieldLabel.Visible = true;
        _descriptionField.Visible = true;
        _saveButton.Visible = true;
        _deleteButton.Visible = true;
    }

    public void SetSource(Character character)
    {
        _character = character; 
    }

    private void OnCharacterUpdate()
    {
        long id = long.Parse(_idField.Text);
        string name = _nameField.Text;
        string? description = _descriptionField.Text;

        if (string.IsNullOrEmpty(name))
        {
            MessageBox.ErrorQuery(App, "Validation error", "Name is required", "Ok");
            return;
        }

        Character model = new(id, name, description);

        CharacterUpdate.Invoke(this, model);
    }

    private void OnCharacterDelete()
    {
        long id = long.Parse(_idField.Text);

        CharacterDelete.Invoke(this, id);
    }
}
