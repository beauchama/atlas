// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Extensions;
using Atlas.Migration.App.Fakes;
using Atlas.Migration.App.Fixtures;
using Atlas.Migration.App.Flags.Converters;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Flags.Dto;

public class FlagDtoTests(SampleDeserializer deserializer) : IClassFixture<SampleDeserializer>
{
    public static TheoryData<string, ICountryData> Countries { get; } = new()
    {
        { nameof(Italy), new Italy() },
        { nameof(SouthAfrica), new SouthAfrica() },
        { nameof(Antarctica), new Antarctica() }
    };

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheCommonCountryName(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Name.Common.Should().Be(countryData.Common);
    }

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheOfficialCountryName(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Name.Official.Should().Be(countryData.Official);
    }

    [Fact]
    public void FlagShouldHaveJsonPropertyNameForCountryCode()
    {
        JsonPropertyNameAttribute? attribute = typeof(FlagDto).GetAttribute<JsonPropertyNameAttribute>(nameof(FlagDto.Code));

        attribute!.Name.Should().Be("cca3");
    }

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheCountryCode(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Code.Should().Be(countryData.CountryCode3);
    }

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheRegion(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Region.Should().Be(countryData.Region);
    }

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheFrenchTranslation(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        (string common, string official) = flag.Translations.French;

        common.Should().Be(countryData.FrenchCommon);
        official.Should().Be(countryData.FrenchOfficial);
    }

    [Fact]
    public void FlagShouldHaveJsonPropertyNameForCoordinate()
    {
        JsonPropertyNameAttribute? attribute = typeof(FlagDto).GetAttribute<JsonPropertyNameAttribute>(nameof(FlagDto.Coordinate));

        attribute!.Name.Should().Be("latlng");
    }

    [Fact]
    public void FlagShouldHaveJsonConverterToConvertCoordinate()
    {
        JsonConverterAttribute? attribute = typeof(FlagDto).GetAttribute<JsonConverterAttribute>(nameof(FlagDto.Coordinate));

        attribute.Should().NotBeNull();
        attribute!.ConverterType.Should().Be<GeographicCoordinateDtoJsonConverter>();
    }

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheGeographicCoordinate(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        (double latitude, double longitude) = flag.Coordinate;

        latitude.Should().Be(countryData.Latitude);
        longitude.Should().Be(countryData.Longitude);
    }

    [Theory, MemberData(nameof(Countries))]
    public void FlagShouldHaveTheArea(string country, ICountryData countryData)
    {
        FlagDto flag = DeserializeToFlagDto(country);

        flag.Area.Should().Be(countryData.Area);
    }

    private FlagDto DeserializeToFlagDto(string country)
        => deserializer.Deserialize(country, FlagDtoJsonContext.Default.FlagDto);
}
