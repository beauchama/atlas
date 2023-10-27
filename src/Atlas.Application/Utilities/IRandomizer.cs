// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Application.Utilities;

internal interface IRandomizer
{
    T Randomize<T>(IEnumerable<T> source);
}
