namespace Koncar.Interview.Client.Infrastructure.Http;

using Koncar.Interview.Client.Application.Contracts.Adapters;
using Koncar.Interview.Client.Application.Contracts.Models;
using Koncar.Interview.Client.Domain.Entities;
using Koncar.Interview.Client.Infrastructure.Http.Internal.Common;
using Koncar.Interview.Client.Infrastructure.Http.Internal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

internal sealed class CharacterServiceAdapter : ICharacterServiceAdapter
{
    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    private readonly HttpClient _httpClient;

    public CharacterServiceAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Character> CreateAsync(
        CreateCharacterModel model,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new(
            HttpMethod.Post,
            Endpoints.V1.Characters.Route)
        {
            Content = new StringContent(
                JsonSerializer.Serialize(model, Options),
                Encoding.UTF8,
                MediaTypeNames.Application.Json)
        };

        using HttpResponseMessage response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        string payload = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<Character>(payload, Options)
            ?? throw new ApplicationException("Something went wrong while deserializing");
    }

    public async Task DeleteAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new(
            HttpMethod.Delete,
            $"{Endpoints.V1.Characters.Route}/{id}");

        using HttpResponseMessage response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<List<Character>> GetAsync(CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new(
            HttpMethod.Get,
            Endpoints.V1.Characters.Route);

        using HttpResponseMessage response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        string payload = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<List<Character>>(payload, Options) ?? [];
    }

    public async Task<Character?> GetAsync(
        long id,
        CancellationToken cancellationToken = default)
    {
        using HttpRequestMessage request = new(
            HttpMethod.Get,
            $"{Endpoints.V1.Characters.Route}/{id}");

        using HttpResponseMessage response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        string payload = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<Character>(payload, Options);
    }

    public async Task<Character> UpdateAsync(
        Character character,
        CancellationToken cancellationToken = default)
    {
        UpdateCharacterModel model = new(
            character.Name,
            character.Description);

        using HttpRequestMessage request = new(
            HttpMethod.Put,
            $"{Endpoints.V1.Characters.Route}/{character.Id}")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(model, Options),
                Encoding.UTF8,
                MediaTypeNames.Application.Json)
        };

        using HttpResponseMessage response = await _httpClient.SendAsync(
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();

        string payload = await response.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<Character>(payload, Options) ??
            throw new ApplicationException("Something went wrong while deserializing.");
    }
}
