// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Flags.Settings;

public sealed record FlagSourceSettings
{
    public const string Section = "sources:flags";

    public required Uri Endpoint { get; init; }
}
