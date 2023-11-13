// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Contracts.Flags;
using MediatR;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Application.Flags;

[ExcludeFromCodeCoverage]
public static class FlagRequests
{
    public sealed record GetAll : IRequest<IEnumerable<Flag>>;

    public sealed record Guess(string FlagCode, string GuessedFlagCode) : IRequest<GuessedFlag>;

    public sealed record Randomize : IRequest<string>;
}
