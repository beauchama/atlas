// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using NSubstitute.ReceivedExtensions;
using System.Text.Json.Serialization.Metadata;

namespace Atlas.Migration.App.Utilities;

public class JsonFileWriterTests
{
    private const string Path = "path";
    private const int Value = 42;

    private readonly JsonTypeInfo<int> _typeInfo = JsonTypeInfo.CreateJsonTypeInfo<int>(new System.Text.Json.JsonSerializerOptions());
    private readonly IFile _file = Substitute.For<IFile>();
    private readonly IJsonSerializer<int> _serializer = Substitute.For<IJsonSerializer<int>>();

    private readonly JsonFileWriter<int> _writer;

    public JsonFileWriterTests()
    {
        _file.OpenWrite(Path).Returns(new MemoryStream());

        _writer = new JsonFileWriter<int>(_file, _serializer);
    }

    [Fact]
    public async Task WriteToAsyncShouldOpenAStreamToWrite()
    {
        await _writer.WriteToAsync(Path, Value, _typeInfo);

        _file.Received(Quantity.Exactly(1)).OpenWrite(Path);
    }

    [Fact]
    public async Task WriteToAsyncShouldDisposeTheStream()
    {
        Stream stream = Substitute.For<Stream>();

        _file.OpenWrite(Path).Returns(stream);

        await _writer.WriteToAsync(Path, Value, _typeInfo);

        await stream.Received(Quantity.Exactly(1)).DisposeAsync();
    }

    [Fact]
    public async Task WriteToAsyncShouldSerializeToTheStream()
    {
        using Stream stream = new MemoryStream();

        _file.OpenWrite(Path).Returns(stream);

        await _writer.WriteToAsync(Path, Value, _typeInfo);

        await _serializer.Received(Quantity.Exactly(1)).SerializeAsync(stream, Value, _typeInfo, default);
    }
}
