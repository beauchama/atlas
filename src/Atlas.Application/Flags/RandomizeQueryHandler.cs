// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Application.Utilities;
using Atlas.Domain.Flags;
using Fluxor;

namespace Atlas.Application.Flags;

internal sealed class RandomizeQueryHandler(IFlagRepository flagRepository, IRandomizer randomizer)
{
    [EffectMethod(typeof(FlagActions.RandomizeRequest))]
    public async Task HandleAsync(IDispatcher dispatcher)
    {
        IEnumerable<Flag> flags = await flagRepository.GetAllAsync().ConfigureAwait(false);

        Flag randomizedFlag = randomizer.Randomize(flags);

        dispatcher.Dispatch(new FlagActions.RandomizeResponse(randomizedFlag.Code));
    }
}
