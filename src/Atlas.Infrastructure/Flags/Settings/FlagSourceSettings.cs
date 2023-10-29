// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Infrastructure.Flags.Settings;

internal sealed record FlagSourceSettings
{
    public const string Section = "sources:flags";

    public required Uri Endpoint { get; init; }
}
