// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using System.Diagnostics.CodeAnalysis;
using SystemStopwatch = System.Diagnostics.Stopwatch;

namespace Atlas.Migration.App.Utilities;

[ExcludeFromCodeCoverage]
internal sealed class Stopwatch : IStopwatch
{
    private readonly SystemStopwatch _stopwatch = new();

    public long ElapsedMilliseconds => _stopwatch.ElapsedMilliseconds;

    public void Reset() => _stopwatch.Reset();

    public void Start() => _stopwatch.Start();

    public void Stop() => _stopwatch.Stop();
}
