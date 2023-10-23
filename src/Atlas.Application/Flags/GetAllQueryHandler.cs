// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Domain.Flags;
using Fluxor;

namespace Atlas.Application.Flags;

internal sealed class GetAllQueryHandler(IFlagRepository flagRepository)
{
    [EffectMethod(typeof(FlagActions.GetAllRequest))]
    public async Task HandleAsync(IDispatcher dispatcher)
    {
        IEnumerable<Flag> flags = await flagRepository.GetAllAsync().ConfigureAwait(false);

        dispatcher.Dispatch(new FlagActions.GetAllResponse(flags.MapToShared()));
    }
}
