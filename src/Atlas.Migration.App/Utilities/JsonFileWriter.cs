// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Text.Json.Serialization.Metadata;

namespace Atlas.Migration.App.Utilities;

internal sealed class JsonFileWriter<T>(IFile file, IJsonSerializer<T> serializer) : IJsonFileWriter<T>
{
    public async Task WriteToAsync(string path, T value, JsonTypeInfo<T> metadata, CancellationToken cancellationToken = default)
    {
        Stream stream = file.OpenWrite(path);

        await using (stream.ConfigureAwait(false))
        {
            await serializer.SerializeAsync(stream, value, metadata, cancellationToken).ConfigureAwait(false);
        }
    }
}
