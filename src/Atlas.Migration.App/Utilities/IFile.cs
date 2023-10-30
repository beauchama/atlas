// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Utilities;

internal interface IFile
{
    Stream OpenWrite(string path);
}
