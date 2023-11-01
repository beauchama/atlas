// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Microsoft.Extensions.Logging;

namespace Atlas.Migration.App;

internal static partial class AtlasApplicationLogging
{
    [LoggerMessage(LogLevel.Information, "Setup the migration...")]
    public static partial void SetupMigration(this ILogger logger);

    [LoggerMessage(LogLevel.Information, "Migrating {data}...")]
    public static partial void MigratingData(this ILogger logger, string data);

    [LoggerMessage(LogLevel.Information, "Migration of {data} completed")]
    public static partial void DataMigrationCompleted(this ILogger logger, string data);

    [LoggerMessage(LogLevel.Information, "Migration has been completed...")]
    public static partial void MigrationCompleted(this ILogger logger);
}
