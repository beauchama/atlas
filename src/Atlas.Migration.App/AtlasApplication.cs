// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure;
using Atlas.Migration.App.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Migration.App;

internal sealed class AtlasApplication(
    IHostEnvironment environment,
    IDirectory directory,
    IHostApplicationLifetime applicationLifetime,
    ILogger<AtlasApplication> logger,
    IEnumerable<IMigrator> migrators)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.SetupMigration();

        string wwwrootPath = GetWwwRootPath();
        string dataPath = directory.Create(Path.Combine(wwwrootPath, JsonPaths.DataFolder));

        foreach (IMigrator migrator in migrators)
        {
            string dataName = Path.GetFileNameWithoutExtension(migrator.Filename);

            logger.MigratingData(dataName);

            await migrator.MigrateAsync(Path.Combine(dataPath, migrator.Filename), cancellationToken).ConfigureAwait(false);

            logger.DataMigrationCompleted(dataName);
        }

        logger.MigrationCompleted();

        applicationLifetime.StopApplication();
    }

    [ExcludeFromCodeCoverage]
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private string GetWwwRootPath()
    {
        string relativePath = environment.IsDevelopment()
            ? "../../../../"
            : "../../";

        string basePath = Path.Combine(environment.ContentRootPath, relativePath);

        return directory.Search(basePath, "wwwroot");
    }
}
