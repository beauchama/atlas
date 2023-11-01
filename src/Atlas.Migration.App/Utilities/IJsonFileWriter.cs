// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Text.Json.Serialization.Metadata;

namespace Atlas.Migration.App.Utilities;

internal interface IJsonFileWriter<T>
{
    Task WriteToAsync(string path, T value, JsonTypeInfo<T> metadata, CancellationToken cancellationToken = default);
}
