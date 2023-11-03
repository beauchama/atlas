// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Infrastructure.Flags.Settings;
using Microsoft.Extensions.Options;
using MockHttp;
using System.Net;
using System.Text.Json;

namespace Atlas.Infrastructure.Flags.Persistence;

public sealed class FlagRepositoryTests : IDisposable
{
    private readonly MockHttpHandler _handler = new();
    private readonly HttpClient _httpClient;
    private readonly FlagSourceSettings _flagSourceSettings = new()
    {
        Endpoint = new Uri("https://localhost/flags"),
    };

    private readonly Flag _canadaFlag = new()
    {
        Code = "can",
        Translations = new Translations()
        {
            English = new Translation("Canada", "Canada"),
            French = new Translation("Canada", "Canada")
        },
        Continent = Continent.America,
        Coordinate = new GeographicCoordinate(60, -95),
        Area = 9984670
    };

    private readonly FlagRepository _flagRepository;

    public FlagRepositoryTests()
    {
        _httpClient = new HttpClient(_handler);
        IOptions<FlagSourceSettings> options = Options.Create(_flagSourceSettings);

        _flagRepository = new FlagRepository(_httpClient, options);
    }

    public void Dispose()
    {
        _handler.Dispose();
        _httpClient.Dispose();
    }

    [Fact]
    public async Task GetAllAsyncShouldGetFromHttp()
    {
        string json = JsonSerializer.Serialize([], FlagJsonContext.Default.IEnumerableFlag);

        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.OK).Body(json));

        _ = await _flagRepository.GetAllAsync();

        await _handler.VerifyAsync(h => h.Method(HttpMethod.Get).RequestUri(_flagSourceSettings.Endpoint), IsSent.Once());
    }

    [Fact]
    public async Task GetAllAsyncShouldGetEmptyArrayWhenIsNotSuccess()
    {
        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.NotFound));

        IEnumerable<Flag> flags = await _flagRepository.GetAllAsync();

        flags.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsyncShouldGetFlags()
    {
        string json = JsonSerializer.Serialize([_canadaFlag], FlagJsonContext.Default.IEnumerableFlag);

        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.OK).Body(json));

        IEnumerable<Flag> flags = await _flagRepository.GetAllAsync();

        flags.Should().ContainSingle();

        Flag flag = flags.First();

        flag.Should().Be(_canadaFlag);
    }

    [Fact]
    public async Task GetAsyncShouldGetFromHttp()
    {
        string json = JsonSerializer.Serialize([_canadaFlag], FlagJsonContext.Default.IEnumerableFlag);

        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.OK).Body(json));

        _ = await _flagRepository.GetAsync(_canadaFlag.Code);

        await _handler.VerifyAsync(h => h.Method(HttpMethod.Get).RequestUri(_flagSourceSettings.Endpoint), IsSent.Once());
    }

    [Fact]
    public Task GetAsyncShouldThrowWhenIsNotSuccess()
    {
        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.NotFound));

        Func<Task> act = async () => await _flagRepository.GetAsync(string.Empty);

        return act.Should().ThrowAsync<HttpRequestException>();
    }

    [Fact]
    public async Task GetAsyncShouldGetTheFlag()
    {
        string json = JsonSerializer.Serialize([_canadaFlag], FlagJsonContext.Default.IEnumerableFlag);

        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.OK).Body(json));

        Flag? flag = await _flagRepository.GetAsync(_canadaFlag.Code);

        flag.Should().Be(_canadaFlag);
    }
}
