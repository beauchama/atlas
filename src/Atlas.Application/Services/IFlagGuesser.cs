// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;

namespace Atlas.Application.Services;

internal interface IFlagGuesser
{
    GuessedFlag Guess(Flag flagToGuess, Flag guessedFlag);
}
