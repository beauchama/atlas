// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Utilities;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Migration.App;

[ExcludeFromCodeCoverage]
internal static class DependencyInjection
{
    internal static IServiceCollection AddIO(this IServiceCollection services)
    {
        return services
            .AddTransient<IFile, Utilities.File>()
            .AddTransient<IDirectory, Utilities.Directory>();
    }

    internal static IServiceCollection ConfigureJson<T>(this IServiceCollection services) where T : class
    {
        return services
            .AddTransient<IJsonSerializer<T>, JsonSerializer<T>>()
            .AddTransient<IJsonFileWriter<T>, JsonFileWriter<T>>();
    }
}
