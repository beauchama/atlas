// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Domain.Translations;
using Atlas.Migration.App.Flags.Dto;
using Atlas.Migration.App.Geography.Dto;
using Atlas.Migration.App.Translations.Dto;

namespace Atlas.Migration.App.Flags.Mappers;

public sealed class FlagMapperTests
{
    private readonly FlagDto _flag = new()
    {
        Code = "can",
        Name = new NameDto("Canada", "Canada"),
        Translations = [new TranslationDto("fra", "Canada", "Canada")],
        Coordinate = new GeographicCoordinateDto(60, -95),
        Area = 9984670,
        Region = "Americas"
    };

    [Fact]
    public void AsDomainShouldConvertDtoToDomain()
    {
        Flag flag = new FlagDto[] { _flag }.AsDomain().First();

        flag.Code.Should().Be(_flag.Code);
        flag.Translations.Should().BeEquivalentTo([new Translation("fra", "Canada", "Canada"), new Translation("eng", "Canada", "Canada")]);
        flag.Continent.Should().Be(Continent.America);
        flag.Coordinate.Latitude.Should().Be(_flag.Coordinate.Latitude);
        flag.Coordinate.Longitude.Should().Be(_flag.Coordinate.Longitude);
        flag.Area.ToDouble().Should().Be(_flag.Area);
    }
}
