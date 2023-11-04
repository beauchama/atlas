// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;
using System.Text;
using System.Text.Json;

namespace Atlas.Infrastructure.Geography.Converters;

public sealed class AreaJsonConverterTests
{
    private readonly AreaJsonConverter _converter = new();

    [Fact]
    public void ReadShouldGetTheAreaFromJson()
    {
        Utf8JsonReader reader = CreateJsonReader(/*lang=json,strict*/"""{ "area": 42.0 }""");

        Area area = _converter.Read(ref reader, typeof(Area), new JsonSerializerOptions())!;

        area.ToDouble().Should().Be(42.0);
    }

    [Fact]
    public void WriteShouldWriteTheAreaToJson()
    {
        using MemoryStream stream = new();
        using Utf8JsonWriter writer = new(stream);

        writer.WriteStartObject();
        writer.WritePropertyName("area");

        _converter.Write(writer, new Area(42.0), new JsonSerializerOptions());

        writer.WriteEndObject();
        writer.Flush();

        Encoding.UTF8.GetString(stream.ToArray()).Should().Be(/*lang=json,strict*/"""{"area":42}""");
    }

    private static Utf8JsonReader CreateJsonReader(string json)
    {
        Utf8JsonReader reader = new(Encoding.UTF8.GetBytes(json));

        while (reader.TokenType != JsonTokenType.Number)
            reader.Read();

        return reader;
    }
}
