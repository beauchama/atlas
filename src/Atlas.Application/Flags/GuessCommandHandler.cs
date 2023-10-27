// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Persistence;
using Atlas.Application.Services;
using Atlas.Domain.Flags;
using Fluxor;

namespace Atlas.Application.Flags;

internal sealed class GuessCommandHandler(IFlagRepository flagRepository, IFlagGuesser flagGuesser)
{
    [EffectMethod]
    public async Task HandleAsync(FlagActions.GuessRequest request, IDispatcher dispatcher)
    {
        Flag flagToGuess = await flagRepository.GetAsync(request.FlagCode).ConfigureAwait(false);
        Flag guessedFlag = await flagRepository.GetAsync(request.GuessedFlagCode).ConfigureAwait(false);

        GuessedFlag flag = flagGuesser.Guess(flagToGuess, guessedFlag);

        dispatcher.Dispatch(new FlagActions.GuessResponse(flag.MapToShared()));
    }
}
