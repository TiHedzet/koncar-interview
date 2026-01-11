namespace Koncar.Interview.Client.Presentation.Tui.Contracts.Views.Characters;

using Koncar.Interview.Client.Application.Contracts.Models;
using System;

public interface ICreateCharacterView
{
    public event EventHandler<CreateCharacterModel> CreateRequested;
}
