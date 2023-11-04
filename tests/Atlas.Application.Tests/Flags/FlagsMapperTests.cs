// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Contracts.Flags;
using Atlas.Contracts.Geography;

namespace Atlas.Application.Flags;

public sealed class FlagsMapperTests
{
    private readonly Domain.Flags.Flag _flag = FakeFlag.CanadaFlag;
    private readonly Domain.Flags.GuessedFlag _guessedFlag = FakeFlag.GuessedCanadaFlag;

    [Fact]
    public void FlagMapToSharedShouldMapEntityToShared()
    {
        IEnumerable<Flag> flags = new[] { _flag }.AsFlagContracts();

        flags.Should().ContainSingle();
        Flag flag = flags.First();

        flag.Code.Should().Be(_flag.Code);
        flag.Translations.English.Name.Should().Be(_flag.Translations.English.Name);
        flag.Translations.English.OfficialName.Should().Be(_flag.Translations.English.OfficialName);
        flag.Translations.French.Name.Should().Be(_flag.Translations.French.Name);
        flag.Translations.French.OfficialName.Should().Be(_flag.Translations.French.OfficialName);
    }

    [Fact]
    public void GuessedFlagMapToSharedShouldMapEntityToShared()
    {
        GuessedFlag flag = _guessedFlag.AsGuessedFlagContract();

        flag.Code.Should().Be(_guessedFlag.Code);
        flag.Translations.English.Name.Should().Be(_guessedFlag.Translations.English.Name);
        flag.Translations.English.OfficialName.Should().Be(_guessedFlag.Translations.English.OfficialName);
        flag.Translations.French.Name.Should().Be(_guessedFlag.Translations.French.Name);
        flag.Translations.French.OfficialName.Should().Be(_guessedFlag.Translations.French.OfficialName);
        flag.IsSuccess.Should().Be(_guessedFlag.IsSuccess);
        flag.SameContinent.Should().Be(_guessedFlag.SameContinent);
        flag.Size.Should().Be(AreaSize.Larger);
        flag.Kilometers.Should().Be(_guessedFlag.Distance.Kilometers);
        flag.Miles.Should().Be(_guessedFlag.Distance.Miles);
        flag.Direction.Should().Be(_guessedFlag.Direction);
    }

    [Theory, ClassData(typeof(Area))]
    public void GuessedFlagShouldReturnTheGoodAreaSize(AreaSize expectedSize, Domain.Geography.AreaSize size)
    {
        GuessedFlag flag = (_guessedFlag with { Size = size }).AsGuessedFlagContract();

        flag.Size.Should().Be(expectedSize);
    }
}

file sealed class Area : TheoryData<AreaSize, Domain.Geography.AreaSize>
{
    public Area()
    {
        Add(AreaSize.Larger, Domain.Geography.AreaSize.Larger);
        Add(AreaSize.Same, Domain.Geography.AreaSize.Same);
        Add(AreaSize.Smaller, Domain.Geography.AreaSize.Smaller);
        Add(AreaSize.Same, (Domain.Geography.AreaSize)999);
    }
}
