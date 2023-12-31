// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Flags.Abstractions;
using Atlas.Domain.Flags;
using Atlas.Domain.Geography;

namespace Atlas.Application.Flags;

internal sealed class FlagGuesser : IFlagGuesser
{
    public GuessedFlag Guess(Flag flagToGuess, Flag guessedFlag)
    {
        return flagToGuess.Code.Equals(guessedFlag.Code, StringComparison.Ordinal)
            ? Guessed(guessedFlag)
            : FailedToGuess(flagToGuess, guessedFlag);
    }

    private static GuessedFlag Guessed(Flag guessedFlag) => new()
    {
        IsSuccess = true,
        Code = guessedFlag.Code,
        Translations = guessedFlag.Translations,
        SameContinent = true,
        Size = AreaSize.Same,
        Direction = Direction.Zero,
        Distance = Distance.Zero
    };

    private static GuessedFlag FailedToGuess(Flag flagToGuess, Flag guessedFlag) => new()
    {
        Code = guessedFlag.Code,
        Translations = guessedFlag.Translations,
        IsSuccess = false,
        SameContinent = flagToGuess.Continent == guessedFlag.Continent,
        Size = flagToGuess.Area.CompareTo(guessedFlag.Area),
        Direction = Direction.Calculate(guessedFlag.Coordinate, flagToGuess.Coordinate),
        Distance = Distance.Calculate(guessedFlag.Coordinate, flagToGuess.Coordinate)
    };
}
