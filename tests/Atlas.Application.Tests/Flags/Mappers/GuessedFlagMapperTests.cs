// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Contracts.Flags;
using Atlas.Contracts.Geography;

namespace Atlas.Application.Flags.Mappers;

public sealed class GuessedFlagMapperTests
{
    [Fact]
    public void AsContractShouldConvertDomainToContract()
    {
        GuessedFlag flag = FakeFlag.GuessedCanadaFlag.AsContract();

        flag.Code.Should().Be(FakeFlag.GuessedCanadaFlag.Code);
        flag.Translations.Should().Contain(t => FakeFlag.GuessedCanadaFlag.Translations.Any(te => te.Code == t.Code));
        flag.IsSuccess.Should().Be(FakeFlag.GuessedCanadaFlag.IsSuccess);
        flag.SameContinent.Should().Be(FakeFlag.GuessedCanadaFlag.SameContinent);
        flag.Size.Should().Be(AreaSize.Larger);
        flag.Kilometers.Should().Be(FakeFlag.GuessedCanadaFlag.Distance.Kilometers);
        flag.Miles.Should().Be(FakeFlag.GuessedCanadaFlag.Distance.Miles);
        flag.Direction.Should().Be(FakeFlag.GuessedCanadaFlag.Direction);
    }
}
