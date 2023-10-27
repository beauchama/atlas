// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Web.Shared.Flags;

namespace Atlas.Application.Flags;

public static class FlagActions
{
    public sealed record GetAllRequest;

    public sealed record GetAllResponse(IEnumerable<Flag> Flags);

    public sealed record GuessRequest(string FlagCode, string GuessedFlagCode);

    public sealed record GuessResponse(GuessedFlag GuessedFlag);

    public sealed record RandomizeRequest;

    public sealed record RandomizeResponse(string Code);
}
