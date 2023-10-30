// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure.Flags.Settings.Validations;
using Atlas.Infrastructure.Settings.Validations;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Infrastructure.Flags.Settings;

[ExcludeFromCodeCoverage]
public static class SettingsExtensions
{
    public static IServiceCollection ConfigureFlagSourceSettings(this IServiceCollection services)
        => services.AddFluentOptions<FlagSourceSettings, FlagSourceSettingsValidator>(FlagSourceSettings.Section);
}
