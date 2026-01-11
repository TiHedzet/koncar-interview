namespace Koncar.Interview.Server.Presentation.Api.Endpoints.Characters.V1;

using Asp.Versioning;
using Koncar.Interview.Server.Application.Characters.Commands;
using Koncar.Interview.Server.Application.Characters.Queries;
using Koncar.Interview.Server.Domain.Entities;
using Koncar.Interview.Server.Presentation.Api.Constants;
using Koncar.Interview.Server.Presentation.Api.Endpoints.Characters.V1.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

[ApiVersion(ApiVersions.V1)]
public sealed class CharactersController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public CharactersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<Character>))]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        GetCharacters.Query query = new();

        List<Character> characters = await _mediator.Send(
            query,
            cancellationToken);

        return Ok(characters);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> GetById(
        long id,
        CancellationToken cancellationToken)
    {
        GetCharacterById.Query query = new(id);

        Character character = await _mediator.Send(
            query,
            cancellationToken);

        return Ok(character);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Create(
        [FromBody] CreateCharacterRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        CreateCharacter.Command command = new(
            request.Name,
            request.Description);

        Character character = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(character);
    }

    [HttpPut("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Update(
        long id,
        [FromBody] UpdateCharacterRequest request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);
        request.Validate();

        UpdateCharacter.Command command = new (
            id,
            request.Name,
            request.Description);

        Character character = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(character);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Character))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesErrorResponseType(typeof(ProblemDetails))]
    public async Task<IActionResult> Delete(
        long id,
        CancellationToken cancellationToken)
    {
        DeleteCharacter.Command command = new(id);

        Character character = await _mediator.Send(
            command,
            cancellationToken);

        return Ok(character);
    }
}
