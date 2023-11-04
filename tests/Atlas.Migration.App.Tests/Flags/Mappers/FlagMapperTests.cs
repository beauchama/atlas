// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Flags;
using Atlas.Domain.Geography;
using Atlas.Migration.App.Flags.Dto;

namespace Atlas.Migration.App.Flags.Mappers;

public sealed class FlagMapperTests
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
        flag.Translations.English.Name.Should().Be(_flag.Name.Common);
        flag.Translations.English.OfficialName.Should().Be(_flag.Name.Official);
        flag.Translations.French.Name.Should().Be(_flag.Translations.French.Common);
        flag.Translations.French.OfficialName.Should().Be(_flag.Translations.French.Official);
        flag.Continent.Should().Be(Continent.America);
        flag.Coordinate.Latitude.Should().Be(_flag.Coordinate.Latitude);
        flag.Coordinate.Longitude.Should().Be(_flag.Coordinate.Longitude);
        flag.Area.ToDouble().Should().Be(_flag.Area);
    }

    [Theory, ClassData(typeof(Continents))]
    public void MapToDomainShouldConvertCorrectlyTheContinent(string region, Continent continent)
    {
        Flag flag = new FlagDto[] { _flag with { Region = region } }.MapToDomain().First();

        flag.Continent.Should().Be(continent);
    }
}

file sealed class Continents : TheoryData<string, Continent>
{
    public Continents()
    {
        Add("Americas", Continent.America);
        Add("Europe", Continent.Europe);
        Add("Asia", Continent.Asia);
        Add("Africa", Continent.Africa);
        Add("Oceania", Continent.Oceania);
        Add("Antarctic", Continent.Antarctic);
        Add("Region", Continent.America);
    }
}
