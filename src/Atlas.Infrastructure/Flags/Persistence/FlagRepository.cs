// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Domain.Flags;
using Atlas.Infrastructure.Flags.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Atlas.Infrastructure.Flags.Persistence;

internal sealed class FlagRepository(HttpClient httpClient, IOptions<FlagSourceSettings> sourceSettings) : IFlagRepository
{
    public async Task<IEnumerable<Flag>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync(sourceSettings.Value.Endpoint, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            return [];

        IEnumerable<Flag>? flags = await response.Content
            .ReadFromJsonAsync(FlagJsonContext.Default.IEnumerableFlag, cancellationToken)
            .ConfigureAwait(false);

        return flags!;
    }

    public async Task<Flag> GetAsync(string code, CancellationToken cancellationToken = default)
    {
        IEnumerable<Flag>? flags = await httpClient
            .GetFromJsonAsync(sourceSettings.Value.Endpoint, FlagJsonContext.Default.IEnumerableFlag, cancellationToken)
            .ConfigureAwait(false);

        return flags!.First(f => f.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
    }
}
