// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application;
using Atlas.Infrastructure;
using Atlas.Web.App;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddInfrastructure(o =>
{
    o.BaseAddress = builder.HostEnvironment.BaseAddress;
    o.IsProduction = builder.HostEnvironment.IsProduction();
});

builder.Services.AddApplication();

await builder.Build().RunAsync().ConfigureAwait(false);
