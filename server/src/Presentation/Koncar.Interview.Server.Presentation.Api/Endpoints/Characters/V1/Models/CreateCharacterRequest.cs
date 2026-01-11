namespace Koncar.Interview.Server.Presentation.Api.Endpoints.Characters.V1.Models;

using Koncar.Interview.Server.Application.Contracts.Common.Exceptions;
using Koncar.Interview.Server.Presentation.Api.Common.Validation;

public sealed record CreateCharacterRequest(string Name, string? Description) : ValidateRecordBase<CreateCharacterRequest>
{
    public override void Validate()
    {
        if (string.IsNullOrEmpty(Name))
        {
            throw new ValidationException("Name is required");
        }

    }
}
