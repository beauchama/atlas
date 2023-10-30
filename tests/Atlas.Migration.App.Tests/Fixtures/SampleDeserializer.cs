// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace Atlas.Migration.App.Fixtures;

public sealed class SampleDeserializer
{
    private readonly Dictionary<string, byte[]> _samples;

    public SampleDeserializer()
    {
        _samples = Directory.GetFiles("Samples")
            .ToDictionary(f => Path.GetFileNameWithoutExtension(f)!, File.ReadAllBytes, StringComparer.OrdinalIgnoreCase);
    }

    internal T Deserialize<T>(string country, JsonTypeInfo<T> metadata) => JsonSerializer.Deserialize(_samples[country], metadata)!;
}
