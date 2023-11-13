// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Application.Flags.Mappers;
using Atlas.Contracts.Flags;
using Mediator;
using FlagDomain = Atlas.Domain.Flags.Flag;
using GuessedFlagDomain = Atlas.Domain.Flags.GuessedFlag;

namespace Atlas.Application.Flags.Handlers;

internal sealed class GuessCommandHandler(IFlagRepository flagRepository, IFlagGuesser flagGuesser) : IRequestHandler<FlagRequests.Guess, GuessedFlag>
{
    public async ValueTask<GuessedFlag> Handle(FlagRequests.Guess request, CancellationToken cancellationToken)
    {
        FlagDomain flagToGuess = await flagRepository.GetAsync(request.FlagCode, cancellationToken).ConfigureAwait(false);
        FlagDomain guessedFlag = await flagRepository.GetAsync(request.GuessedFlagCode, cancellationToken).ConfigureAwait(false);

        GuessedFlagDomain flag = flagGuesser.Guess(flagToGuess, guessedFlag);

        return flag.AsContract();
    }
}
