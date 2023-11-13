// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags;
using Atlas.Application.Flags.Abstractions;
using Atlas.Application.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Application;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services.AddTransient<IRandomizer, Randomizer>()
            .AddTransient<IFlagGuesser, FlagGuesser>()
            .AddMediator();
    }
}
