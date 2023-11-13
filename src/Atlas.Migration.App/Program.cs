// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App;
using Microsoft.Extensions.Hosting;
using System.Diagnostics.CodeAnalysis;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();

builder.Logging.ConfigureLogging();

builder.Services
    .AddApp()
    .AddFlagsMigration(builder.Environment);

await builder.Build().RunAsync().ConfigureAwait(false);

[ExcludeFromCodeCoverage]
internal static partial class Program
{
}
