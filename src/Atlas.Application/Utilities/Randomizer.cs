// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace Atlas.Application.Utilities;

[ExcludeFromCodeCoverage]
internal sealed class Randomizer : IRandomizer
{
    public T Randomize<T>(IEnumerable<T> source)
        => RandomNumberGenerator.GetItems<T>(source.ToArray(), 1)[0];
}
