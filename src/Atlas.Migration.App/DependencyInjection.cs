// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Infrastructure.Settings.Validations;
using Atlas.Migration.App.Flags;
using Atlas.Migration.App.Flags.Settings;
using Atlas.Migration.App.Flags.Settings.Validations;
using Atlas.Migration.App.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Atlas.Migration.App;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    internal static IServiceCollection AddFlagsMigration(this IServiceCollection services, IHostEnvironment environment)
    {
        IHttpClientBuilder builder = services.AddHttpClient<IFlagRetriever, FlagRetriever>(client =>
        {
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        });

        if (!environment.IsDevelopment())
            _ = builder.RemoveAllLoggers();

        return services
            .AddTransient<IMigrator, FlagMigrator>()
            .ConfigureJson<IEnumerable<Flag>>()
            .AddFluentOptions<FlagSourceSettings, FlagSourceSettingsValidator>(FlagSourceSettings.Section);
    }

    internal static IServiceCollection AddApp(this IServiceCollection services)
    {
        return services
            .AddHostedService<AtlasApplication>()
            .Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true)
            .AddTransient<IFile, Utilities.File>()
            .AddTransient<IDirectory, Utilities.Directory>()
            .AddTransient<IStopwatch, Stopwatch>();
    }

    internal static ILoggingBuilder ConfigureLogging(this ILoggingBuilder builder)
    {
        return builder.ClearProviders()
            .AddSimpleConsole(c => c.SingleLine = true);
    }

    private static IServiceCollection ConfigureJson<T>(this IServiceCollection services) where T : class
    {
        return services
            .AddTransient<IJsonSerializer<T>, JsonSerializer<T>>()
            .AddTransient<IJsonFileWriter<T>, JsonFileWriter<T>>();
    }
}
