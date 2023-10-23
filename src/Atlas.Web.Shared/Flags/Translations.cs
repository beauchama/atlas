// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Web.Shared.Flags;

public sealed record Translations
{
    public required Translation French { get; init; }

    public required Translation English { get; init; }
}
