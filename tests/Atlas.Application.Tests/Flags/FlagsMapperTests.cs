// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Web.Shared.Flags;

namespace Atlas.Application.Flags;

public class FlagsMapperTests
{
    private readonly Domain.Flags.Flag _flag = new()
    {
        Code = "can",
        Translations = new Domain.Flags.Translations()
        {
            English = new Domain.Flags.Translation("eng", "Canada", "Canada"),
            French = new Domain.Flags.Translation("fra", "Canada", "Canada")
        },
        Continent = Domain.Flags.Continent.America,
        Coordinate = new Domain.Geography.GeographicCoordinate(42.0, 42.0),
        Area = 10.0,
    };

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
