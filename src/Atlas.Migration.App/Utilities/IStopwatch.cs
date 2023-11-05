// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Utilities;

internal interface IStopwatch
{
    long ElapsedMilliseconds { get; }

    void Reset();

    void Start();

    void Stop();
}
