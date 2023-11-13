// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Infrastructure.Flags.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Atlas.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<InfrastructureOptions> configure)
    {
        InfrastructureOptions options = new();
        configure(options);

        IHttpClientBuilder builder = services.AddHttpClient<IFlagRepository, FlagRepository>(client =>
        {
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
            client.BaseAddress = new Uri(options.BaseAddress);
        });

        if (options.IsProduction)
            _ = builder.RemoveAllLoggers();

        return services
            .AddMemoryCache()
            .Decorate<IFlagRepository, CachedFlagRepository>();
    }
}
