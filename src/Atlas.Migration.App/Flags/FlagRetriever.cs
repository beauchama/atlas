// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Flags.Settings;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace Atlas.Migration.App.Flags;

internal sealed class FlagRetriever(HttpClient httpClient, IOptions<FlagSourceSettings> sourceSettings) : IFlagRetriever
{
    public async Task<IEnumerable<FlagDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using HttpResponseMessage response = await httpClient.GetAsync(sourceSettings.Value.Endpoint, cancellationToken).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            return [];

        IEnumerable<FlagDto>? flags = await response.Content
            .ReadFromJsonAsync(FlagDtoJsonContext.Default.IEnumerableFlagDto, cancellationToken)
            .ConfigureAwait(false);

        return flags!;
    }
}
