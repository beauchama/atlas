// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure.Flags.Settings;
using Atlas.Migration.App.Fakes;
using Atlas.Migration.App.Fixtures;
using Atlas.Migration.App.Flags.Dto;
using Microsoft.Extensions.Options;
using MockHttp;
using System.Net;
using System.Text.Json;

namespace Atlas.Migration.App.Flags;

public sealed class FlagRetrieverTests : IClassFixture<SampleDeserializer>, IDisposable
{
    private readonly MockHttpHandler _handler = new();
    private readonly HttpClient _httpClient;
    private readonly FlagSourceSettings _flagSourceSettings = new()
    {
        Endpoint = new Uri("http://localhost")
    };

    private readonly SampleDeserializer _sampleDeserializer;
    private readonly FlagRetriever _retriever;

    public FlagRetrieverTests(SampleDeserializer sampleDeserializer)
    {
        _sampleDeserializer = sampleDeserializer;

        _httpClient = new HttpClient(_handler);

        IOptions<FlagSourceSettings> options = Options.Create(_flagSourceSettings);

        _retriever = new FlagRetriever(_httpClient, options);
    }

    public void Dispose()
    {
        _handler.Dispose();
        _httpClient.Dispose();
    }

    [Fact]
    public async Task GetAllAsyncShouldGetFromHttp()
    {
        string json = JsonSerializer.Serialize([], FlagDtoJsonContext.Default.IEnumerableFlagDto);

        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.OK).Body(json));

        _ = await _retriever.GetAllAsync();

        await _handler.VerifyAsync(h => h.Method(HttpMethod.Get).RequestUri(_flagSourceSettings.Endpoint), IsSent.Once());
    }

    [Fact]
    public async Task GetAllAsyncShouldGetEmptyArrayWhenIsNotSuccess()
    {
        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.NotFound));

        IEnumerable<FlagDto> flags = await _retriever.GetAllAsync();

        flags.Should().BeEmpty();
    }

    [Fact]
    public async Task GetAllAsyncShouldGetFlags()
    {
        string json = _sampleDeserializer.GetSampleAsJson(nameof(Italy));
        FlagDto italy = _sampleDeserializer.Deserialize(nameof(Italy), FlagDtoJsonContext.Default.FlagDto);

        _handler
            .When(x => x.RequestUri(_flagSourceSettings.Endpoint))
            .Respond(x => x.StatusCode(HttpStatusCode.OK).Body(json));

        IEnumerable<FlagDto> flags = await _retriever.GetAllAsync();

        flags.Should().ContainSingle();

        FlagDto flag = flags.First();

        flag.Code.Should().Be(italy.Code);
        flag.Name.Should().Be(italy.Name);
        flag.Translations.Should().BeEquivalentTo(italy.Translations);
        flag.Region.Should().Be(italy.Region);
        flag.Coordinate.Should().Be(italy.Coordinate);
        flag.Area.Should().Be(italy.Area);
    }
}
