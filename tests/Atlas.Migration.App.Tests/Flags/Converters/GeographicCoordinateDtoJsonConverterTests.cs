// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Flags.Dto;
using System.Text;
using System.Text.Json;

namespace Atlas.Migration.App.Flags.Converters;

public sealed class GeographicCoordinateDtoJsonConverterTests
{
    private readonly GeographicCoordinateDtoJsonConverter _converter = new();

    [Fact]
    public void ReadShouldGetTheLatitudeFromJson()
    {
        Utf8JsonReader reader = CreateJsonReader(/*lang=json,strict*/ """{ "latlng": [ 42.83333333, 12.83333333 ] }""");

        (double latitude, _) = _converter.Read(ref reader, typeof(GeographicCoordinateDto), new JsonSerializerOptions())!;

        latitude.Should().Be(42.83333333);
    }

    [Fact]
    public void ReadShouldGetTheLongitudeFromJson()
    {
        Utf8JsonReader reader = CreateJsonReader(/*lang=json,strict*/ """{ "latlng": [ 42.83333333, 12.83333333 ] }""");

        (_, double longitude) = _converter.Read(ref reader, typeof(GeographicCoordinateDto), new JsonSerializerOptions())!;

        longitude.Should().Be(12.83333333);
    }

    [Fact]
    public void ReadShouldFinishToEndArray()
    {
        Utf8JsonReader reader = CreateJsonReader(/*lang=json,strict*/ """{ "latlng": [ 42.83333333, 12.83333333 ] }""");

        _ = _converter.Read(ref reader, typeof(GeographicCoordinateDto), new JsonSerializerOptions());

        reader.TokenType.Should().Be(JsonTokenType.EndArray);
    }

    [Fact]
    public void WriteShouldThrowNotSupportedException()
    {
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream);

        Action act = () => _converter.Write(writer, new GeographicCoordinateDto(0.0, 0.0), new JsonSerializerOptions());

        act.Should().ThrowExactly<NotSupportedException>();
    }

    private static Utf8JsonReader CreateJsonReader(string json)
    {
        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(json));

        while (reader.TokenType != JsonTokenType.StartArray)
            reader.Read();

        return reader;
    }
}
