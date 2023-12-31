// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Migration.App.Geography.Dto;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Atlas.Migration.App.Geography.Converters;

internal sealed class GeographicCoordinateDtoJsonConverter : JsonConverter<GeographicCoordinateDto>
{
    public override GeographicCoordinateDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        double latitude = SkipPropertyAndGetValue(ref reader);
        double longitude = SkipPropertyAndGetValue(ref reader);

        _ = reader.Read();

        return new GeographicCoordinateDto(latitude, longitude);

        static double SkipPropertyAndGetValue(ref Utf8JsonReader reader)
        {
            _ = reader.Read();
            return reader.GetDouble();
        }
    }

    public override void Write(Utf8JsonWriter writer, GeographicCoordinateDto value, JsonSerializerOptions options)
        => throw new NotSupportedException();
}
