// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace Atlas.Migration.App.Utilities;

[ExcludeFromCodeCoverage]
internal sealed class File : IFile
{
    public Stream OpenWrite(string path) => System.IO.File.OpenWrite(path);
}
