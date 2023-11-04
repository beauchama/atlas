// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Contracts.Flags;
using MediatR;

namespace Atlas.Application.Flags.Handlers;

internal sealed class GuessCommandHandler(IFlagRepository flagRepository, IFlagGuesser flagGuesser) : IRequestHandler<FlagRequests.Guess, GuessedFlag>
{
    public async Task<GuessedFlag> Handle(FlagRequests.Guess request, CancellationToken cancellationToken)
    {
        Domain.Flags.Flag flagToGuess = await flagRepository.GetAsync(request.FlagCode, cancellationToken).ConfigureAwait(false);
        Domain.Flags.Flag guessedFlag = await flagRepository.GetAsync(request.GuessedFlagCode, cancellationToken).ConfigureAwait(false);

        Domain.Flags.GuessedFlag flag = flagGuesser.Guess(flagToGuess, guessedFlag);

        return flag.AsGuessedFlagContract();
    }
}
