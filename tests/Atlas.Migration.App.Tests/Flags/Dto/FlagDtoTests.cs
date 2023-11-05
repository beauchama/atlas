// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Extensions;
using Atlas.Migration.App.Fakes;
using Atlas.Migration.App.Fixtures;
using Atlas.Migration.App.Translations.Dto;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags.Dto;

public sealed class FlagDtoTests(SampleDeserializer deserializer) : IClassFixture<SampleDeserializer>
{
    [Fact]
    public void FlagShouldHaveJsonPropertyNameForCountryCode()
    {
        JsonPropertyNameAttribute? attribute = typeof(FlagDto).GetAttribute<JsonPropertyNameAttribute>(nameof(FlagDto.Code));

        attribute!.Name.Should().Be("cca3");
    }

    [Theory, ClassData(typeof(Countries))]
    public void FlagShouldHaveTheCountryCode(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Code.Should().Be(countryData.CountryCode3);
    }

    [Theory, ClassData(typeof(Countries))]
    public void FlagShouldHaveTheName(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Name.Common.Should().Be(countryData.Common);
        flag.Name.Official.Should().Be(countryData.Official);
    }

    [Theory, ClassData(typeof(Countries))]
    public void FlagShouldHaveTheRegion(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Region.Should().Be(countryData.Region);
    }

    [Theory, ClassData(typeof(Countries))]
    public void FlagShouldHaveTranslations(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        TranslationDto translation = flag.Translations.First(t => t.Code.Equals("fra", StringComparison.Ordinal));

        translation.Code.Should().Be("fra");
        translation.Common.Should().Be(countryData.FrenchCommon);
        translation.Official.Should().Be(countryData.FrenchOfficial);
    }

    [Fact]
    public void FlagShouldHaveJsonPropertyNameForCoordinate()
    {
        JsonPropertyNameAttribute? attribute = typeof(FlagDto).GetAttribute<JsonPropertyNameAttribute>(nameof(FlagDto.Coordinate));

        attribute!.Name.Should().Be("latlng");
    }

    [Theory, ClassData(typeof(Countries))]
    public void FlagShouldHaveTheGeographicCoordinate(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        (double latitude, double longitude) = flag.Coordinate;

        latitude.Should().Be(countryData.Latitude);
        longitude.Should().Be(countryData.Longitude);
    }

    [Theory, ClassData(typeof(Countries))]
    public void FlagShouldHaveTheArea(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Area.Should().Be(countryData.Area);
    }

    private FlagDto DeserializeToFlagDto(string country)
        => deserializer.Deserialize(country, FlagDtoJsonContext.Default.FlagDto);

    internal sealed class Countries : TheoryData<string, ICountryData>
    {
        public Countries()
        {
            Add(nameof(Italy), new Italy());
            Add(nameof(SouthAfrica), new SouthAfrica());
            Add(nameof(Antarctica), new Antarctica());
        }
    }
}
