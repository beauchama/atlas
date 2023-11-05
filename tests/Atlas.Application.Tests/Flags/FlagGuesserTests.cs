// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Domain.Flags;
using Atlas.Domain.Geography;

namespace Atlas.Application.Flags;

public sealed class FlagGuesserTests
{
    private readonly FlagGuesser _flagGuesser = new();

    [Fact]
    public void GuessShouldReturnASuccessGuessedFlagWhenCodeAreTheSame()
    {
        GuessedFlag guessedFlag = _flagGuesser.Guess(FakeFlag.CanadaFlag, FakeFlag.CanadaFlag);

        guessedFlag.IsSuccess.Should().BeTrue();
        guessedFlag.Code.Should().Be(FakeFlag.CanadaFlag.Code);
        guessedFlag.Translations.Should().BeEquivalentTo(FakeFlag.CanadaFlag.Translations);
        guessedFlag.Distance.Should().Be(Distance.Zero);
        guessedFlag.Direction.Should().Be(Direction.Zero);
        guessedFlag.SameContinent.Should().BeTrue();
        guessedFlag.Size.Should().Be(AreaSize.Same);
    }

    [Fact]
    public void GuessShouldReturnAFailedGuessedFlagWhenCodeAreNotTheSame()
    {
        GuessedFlag guessedFlag = _flagGuesser.Guess(FakeFlag.CanadaFlag, FakeFlag.ItalyFlag);

        guessedFlag.IsSuccess.Should().BeFalse();
        guessedFlag.Code.Should().Be(FakeFlag.ItalyFlag.Code);
        guessedFlag.Translations.Should().BeEquivalentTo(FakeFlag.ItalyFlag.Translations);

        guessedFlag.Distance.Kilometers.Should().BeApproximately(6843.3, 0.5);
        guessedFlag.Distance.Miles.Should().BeApproximately(4252.2, 0.5);

        guessedFlag.Direction.ToDouble().Should().Be(284.0);

        guessedFlag.SameContinent.Should().BeFalse();
        guessedFlag.Size.Should().Be(AreaSize.Larger);
    }

    [Fact]
    public void GuessShouldReturnTrueForSameContinentWhenFlagsAreInTheSameContinent()
    {
        GuessedFlag guessedFlag = _flagGuesser.Guess(FakeFlag.UsaFlag, FakeFlag.CanadaFlag);

        guessedFlag.SameContinent.Should().BeTrue();
    }

    [Fact]
    public void GuessShouldReturnFalseForSameContinentWhenFlagsAreNotInTheSameContinent()
    {
        GuessedFlag guessedFlag = _flagGuesser.Guess(FakeFlag.UsaFlag, FakeFlag.ItalyFlag);

        guessedFlag.SameContinent.Should().BeFalse();
    }

    [Fact]
    public void GuessShouldReturnSmallerForAreaSizeWhenGuessedFlagIsLargerThanFlagToGuess()
    {
        GuessedFlag guessedFlag = _flagGuesser.Guess(FakeFlag.UsaFlag, FakeFlag.CanadaFlag);

        guessedFlag.Size.Should().Be(AreaSize.Smaller);
    }

    [Fact]
    public void GuessShouldReturnLargerForAreaSizeWhenGuessedFlagIsSmallerThanFlagToGuess()
    {
        GuessedFlag guessedFlag = _flagGuesser.Guess(FakeFlag.CanadaFlag, FakeFlag.UsaFlag);

        guessedFlag.Size.Should().Be(AreaSize.Larger);
    }
}
