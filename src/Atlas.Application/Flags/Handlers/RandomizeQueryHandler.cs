// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Application.Utilities;
using Atlas.Domain.Flags;
using MediatR;

namespace Atlas.Application.Flags.Handlers;

internal sealed class RandomizeQueryHandler(IFlagRepository flagRepository, IRandomizer randomizer) : IRequestHandler<FlagRequests.Randomize, string>
{
    public async Task<string> Handle(FlagRequests.Randomize request, CancellationToken cancellationToken)
    {
        IEnumerable<Flag> flags = await flagRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        return randomizer.Randomize(flags).Code;
    }
}
