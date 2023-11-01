// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;

namespace Atlas.Migration.App.Utilities;

[ExcludeFromCodeCoverage]
internal sealed class Directory : IDirectory
{
    public string Create(string path) => System.IO.Directory.CreateDirectory(path).FullName;

    public string Search(string path, string pattern)
    {
        EnumerationOptions options = new() { RecurseSubdirectories = true };

        string[] directories = System.IO.Directory.GetDirectories(path, pattern, options);

        return directories.FirstOrDefault() ?? string.Empty;
    }
}
