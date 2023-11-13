// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Application.Flags.Mappers;
using Atlas.Contracts.Flags;
using Mediator;
using FlagDomain = Atlas.Domain.Flags.Flag;

namespace Atlas.Application.Flags.Handlers;

internal sealed class GetAllQueryHandler(IFlagRepository flagRepository) : IRequestHandler<FlagRequests.GetAll, IEnumerable<Flag>>
{
    public async ValueTask<IEnumerable<Flag>> Handle(FlagRequests.GetAll request, CancellationToken cancellationToken)
    {
        IEnumerable<FlagDomain> flags = await flagRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        return flags.AsContract();
    }
}
