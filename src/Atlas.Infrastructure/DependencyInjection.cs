// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Infrastructure.Flags.Persistence;
using Atlas.Infrastructure.Flags.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Atlas.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IHostEnvironment environment)
    {
        IHttpClientBuilder builder = services.AddHttpClient<IFlagRepository, FlagRepository>(client =>
        {
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        });

        if (!environment.IsDevelopment())
            _ = builder.RemoveAllLoggers();

        return services
            .AddMemoryCache()
            .Decorate<IFlagRepository, CachedFlagRepository>()
            .ConfigureFlagSourceSettings();
    }
}
