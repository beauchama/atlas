// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Infrastructure;
using Atlas.Infrastructure.Flags;
using Atlas.Migration.App.Fakes;
using Atlas.Migration.App.Fixtures;
using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Utilities;

namespace Atlas.Migration.App.Flags;

public class FlagMigratorTests : IClassFixture<SampleDeserializer>
{
    private readonly IJsonFileWriter<IEnumerable<Flag>> _jsonFileWriter = Substitute.For<IJsonFileWriter<IEnumerable<Flag>>>();
    private readonly IFlagRetriever _flagRetriever = Substitute.For<IFlagRetriever>();
    private readonly SampleDeserializer _sampleDeserializer;

    private readonly FlagMigrator _migrator;

    public FlagMigratorTests(SampleDeserializer sampleDeserializer)
    {
        _sampleDeserializer = sampleDeserializer;

        _migrator = new FlagMigrator(_flagRetriever, _jsonFileWriter);
    }

    [Fact]
    public void FlagMigratorShouldHaveTheGoodFilename()
    {
        _migrator.Filename.Should().Be(JsonPaths.Flags);
    }

    [Fact]
    public async Task MigrateAsyncShouldCallTheFlagRetriever()
    {
        await _migrator.MigrateAsync(JsonPaths.Flags, default);

        await _flagRetriever.Received(1).GetAllAsync(default);
    }

    [Fact]
    public async Task MigrateAsyncShouldWriteToJsonUsingJsonFileWriter()
    {
        FlagDto italy = _sampleDeserializer.Deserialize(nameof(Italy), FlagDtoJsonContext.Default.FlagDto);
        IEnumerable<FlagDto> flags = [italy];

        _flagRetriever.GetAllAsync(default).Returns(flags);

        await _migrator.MigrateAsync(JsonPaths.Flags, default);

        await _jsonFileWriter.Received(1).WriteToAsync(JsonPaths.Flags, Arg.Is<IEnumerable<Flag>>(x => x.First().Code == italy.Code), FlagJsonContext.Default.IEnumerableFlag, default);
    }
}
