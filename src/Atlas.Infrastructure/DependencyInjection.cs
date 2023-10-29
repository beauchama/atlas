// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Persistence;
using Atlas.Infrastructure.Flags.Persistence;
using Atlas.Infrastructure.Flags.Settings;
using Atlas.Infrastructure.Flags.Settings.Validations;
using Atlas.Infrastructure.Settings.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Infrastructure;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        _ = services.AddHttpClient<IFlagRepository, FlagRepository>();

        return services.AddFluentOptions<FlagSourceSettings, FlagSourceSettingsValidator>(FlagSourceSettings.Section);
    }
}
