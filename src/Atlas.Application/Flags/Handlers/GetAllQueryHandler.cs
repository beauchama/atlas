// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Contracts.Flags;
using MediatR;

namespace Atlas.Application.Flags.Handlers;

internal sealed class GetAllQueryHandler(IFlagRepository flagRepository) : IRequestHandler<FlagRequests.GetAll, IEnumerable<Flag>>
{
    public async Task<IEnumerable<Flag>> Handle(FlagRequests.GetAll request, CancellationToken cancellationToken)
    {
        IEnumerable<Domain.Flags.Flag> flags = await flagRepository.GetAllAsync(cancellationToken).ConfigureAwait(false);

        return flags.AsFlagContracts();
    }
}
