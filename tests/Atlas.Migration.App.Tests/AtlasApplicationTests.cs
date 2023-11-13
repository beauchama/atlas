// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure;
using Atlas.Migration.App.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NSubstitute.ReceivedExtensions;

namespace Atlas.Migration.App;

public sealed class AtlasApplicationTests
{
    private readonly IHostEnvironment _environment = Substitute.For<IHostEnvironment>();
    private readonly IDirectory _directory = Substitute.For<IDirectory>();
    private readonly IHostApplicationLifetime _applicationLifetime = Substitute.For<IHostApplicationLifetime>();
    private readonly IStopwatch _stopwatch = Substitute.For<IStopwatch>();
    private readonly IMigrator _flagMigrator = Substitute.For<IMigrator>();
    private readonly IMigrator _countryMigrator = Substitute.For<IMigrator>();

    private readonly AtlasApplication _atlas;

    public AtlasApplicationTests()
    {
        ILogger<AtlasApplication> logger = Substitute.For<ILogger<AtlasApplication>>();

        _flagMigrator.Filename.Returns("flags.json");
        _countryMigrator.Filename.Returns("countries.json");

        _atlas = new AtlasApplication(_environment, _directory, _applicationLifetime, _stopwatch, logger, [_flagMigrator, _countryMigrator]);
    }

    [Theory, ClassData(typeof(EnvironmentPaths))]
    public async Task StartAsyncShouldSearchWwwRootWithTheGoodPathDependingOfTheEnvironment(string path, string environment)
    {
        const string rootPath = "a/b/c/d/e/f/g";

        _environment.ContentRootPath.Returns(rootPath);
        _environment.EnvironmentName.Returns(environment);

        await _atlas.StartAsync(CancellationToken.None);

        _directory.Received(Quantity.Exactly(1)).Search($"{rootPath}\\{path}", "wwwroot");
    }

    [Fact]
    public async Task StartAsyncShouldCreateTheDataFolder()
    {
        _environment.EnvironmentName.Returns(Environments.Production);
        _directory.Search(Arg.Any<string>(), "wwwroot").Returns("wwwroot");

        await _atlas.StartAsync(CancellationToken.None);

        _directory.Received(Quantity.Exactly(1)).Create($"wwwroot\\{JsonPaths.DataFolder}");
    }

    [Fact]
    public async Task StartAsyncShouldMigrateFromAListOfMigrator()
    {
        string flagsPath = $"{JsonPaths.DataFolder}\\{_flagMigrator.Filename}";
        string countriesPath = $"{JsonPaths.DataFolder}\\{_countryMigrator.Filename}";

        _directory.Create(Arg.Any<string>()).Returns(JsonPaths.DataFolder);

        await _atlas.StartAsync(CancellationToken.None);

        await _flagMigrator.Received(Quantity.Exactly(1)).MigrateAsync(flagsPath, CancellationToken.None);
        await _countryMigrator.Received(Quantity.Exactly(1)).MigrateAsync(countriesPath, CancellationToken.None);
    }

    [Fact]
    public async Task StartAsyncShouldStopApplication()
    {
        await _atlas.StartAsync(CancellationToken.None);

        _applicationLifetime.Received(Quantity.Exactly(1)).StopApplication();
    }
}

file sealed class EnvironmentPaths : TheoryData<string, string>
{
    public EnvironmentPaths()
    {
        Add("../../../../", Environments.Development);
        Add("../../", Environments.Production);
    }
}
