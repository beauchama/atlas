// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Web.Shared.Flags;

namespace Atlas.Application.Flags;

public static class FlagActions
{
    public sealed record GetAllRequest;

    public sealed record GetAllResponse(IEnumerable<Flag> Flags);
}
