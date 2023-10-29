// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Persistence;
using Atlas.Infrastructure.Flags.Persistence;
using Atlas.Infrastructure.Flags.Settings;
using Atlas.Infrastructure.Flags.Settings.Validations;
using Atlas.Infrastructure.Settings.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace Atlas.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddHttpClient<IFlagRepository, FlagRepository>(client =>
        {
            client.DefaultRequestVersion = HttpVersion.Version20;
            client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;
        });

        return services.AddFluentOptions<FlagSourceSettings, FlagSourceSettingsValidator>(FlagSourceSettings.Section);
    }
}
