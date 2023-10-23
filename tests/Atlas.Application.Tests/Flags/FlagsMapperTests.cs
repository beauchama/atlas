// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Application.Fakes;
using Atlas.Web.Shared.Flags;

namespace Atlas.Application.Flags;

public class FlagsMapperTests
{
    private readonly Domain.Flags.Flag _flag = FakeFlag.CanadaFlag;

    [Fact]
    public void MapToSharedShouldMapEntityToShared()
    {
        IEnumerable<Flag> flags = new[] { _flag }.MapToShared();

        flags.Should().ContainSingle();
        Flag flag = flags.First();

        flag.Code.Should().Be(_flag.Code);
        flag.Translations.English.Code.Should().Be(_flag.Translations.English.Code);
        flag.Translations.English.Name.Should().Be(_flag.Translations.English.Name);
        flag.Translations.English.OfficialName.Should().Be(_flag.Translations.English.OfficialName);
        flag.Translations.French.Code.Should().Be(_flag.Translations.French.Code);
        flag.Translations.French.Name.Should().Be(_flag.Translations.French.Name);
        flag.Translations.French.OfficialName.Should().Be(_flag.Translations.French.OfficialName);
    }
}
