// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Logging.ClearProviders().AddConsole();

builder.Services
    .Configure<ConsoleLifetimeOptions>(options => options.SuppressStatusMessages = true)
    .AddFlagsMigration()
    .AddIO();

await builder.Build().RunAsync().ConfigureAwait(false);

[ExcludeFromCodeCoverage]
internal static partial class Program
{
}
