// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Contracts.Flags;

namespace Atlas.Application.Flags.Mappers;

public sealed class FlagMapperTests
{
    [Fact]
    public void AsContractShouldConvertDomainToContract()
    {
        IEnumerable<Flag> flags = new[] { FakeFlag.CanadaFlag }.AsContract();

        Flag flag = flags.First();

        flag.Code.Should().Be(FakeFlag.CanadaFlag.Code);
        flag.Translations.Should().Contain(t => FakeFlag.CanadaFlag.Translations.Any(td => td.Code == t.Code));
    }
}
