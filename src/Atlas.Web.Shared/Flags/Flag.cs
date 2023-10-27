// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Web.Shared.Flags;

public sealed record Flag
{
    public required string Code { get; init; }

    public required Translations Translations { get; init; }
}
