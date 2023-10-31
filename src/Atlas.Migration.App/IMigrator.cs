// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App;

internal interface IMigrator
{
    string Filename { get; }

    Task MigrateAsync(string path, CancellationToken cancellationToken = default);
}
