// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Migration.App.Flags.Dto;

namespace Atlas.Migration.App.Flags.Mappers;

public class FlagMapperTests
{
    private readonly FlagDto _flag = new()
    {
        Code = "can",
        Name = new TranslationDto("Canada", "Canada"),
        Translations = new TranslationsDto { French = new TranslationDto("Canada", "Canada") },
        Coordinate = new GeographicCoordinateDto(60, -95),
        Area = 9984670,
        Region = "Americas"
    };

    [Fact]
    public void MapToDomainShouldConvertDtoToDomain()
    {
        Flag flag = new FlagDto[] { _flag }.MapToDomain().First();

        flag.Code.Should().Be(_flag.Code);
        flag.Translations.English.Code.Should().Be("eng");
        flag.Translations.English.Name.Should().Be(_flag.Name.Common);
        flag.Translations.English.OfficialName.Should().Be(_flag.Name.Official);
        flag.Translations.French.Code.Should().Be("fra");
        flag.Translations.French.Name.Should().Be(_flag.Translations.French.Common);
        flag.Translations.French.OfficialName.Should().Be(_flag.Translations.French.Official);
        flag.Continent.Should().Be(Continent.America);
        flag.Coordinate.Latitude.Should().Be(_flag.Coordinate.Latitude);
        flag.Coordinate.Longitude.Should().Be(_flag.Coordinate.Longitude);
        flag.Area.Should().Be(_flag.Area);
    }

    [Theory]
    [InlineData("Americas", Continent.America)]
    [InlineData("Europe", Continent.Europe)]
    [InlineData("Asia", Continent.Asia)]
    [InlineData("Africa", Continent.Africa)]
    [InlineData("Oceania", Continent.Oceania)]
    [InlineData("Antarctic", Continent.Antarctic)]
    [InlineData("Region", Continent.America)]
    public void MapToDomainShouldConvertCorrectlyTheContinent(string region, Continent continent)
    {
        Flag flag = new FlagDto[] { _flag with { Region = region } }.MapToDomain().First();

        flag.Continent.Should().Be(continent);
    }
}
