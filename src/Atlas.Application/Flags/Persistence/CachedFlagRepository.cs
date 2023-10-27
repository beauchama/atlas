// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Microsoft.Extensions.Caching.Memory;

namespace Atlas.Application.Flags.Persistence;

internal sealed class CachedFlagRepository(IFlagRepository flagRepository, IMemoryCache memoryCache) : IFlagRepository
{
    private const string FlagsKey = "flags";

    public async Task<IEnumerable<Flag>> GetAllAsync()
    {
        if (memoryCache.TryGetValue(FlagsKey, out IEnumerable<Flag>? cachedFlags))
        {
            return cachedFlags!;
        }

        IEnumerable<Flag> flags = await flagRepository.GetAllAsync().ConfigureAwait(false);

        return memoryCache.Set(FlagsKey, flags);
    }

    public async Task<Flag> GetAsync(string code)
    {
        if (memoryCache.TryGetValue(code, out Flag? cachedFlag))
        {
            return cachedFlag!;
        }

        Flag flag = await flagRepository.GetAsync(code).ConfigureAwait(false);

        return memoryCache.Set(code, flag);
    }
}
