// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Domain.Extensions;

internal static class MathExtensions
{
    internal static double ToRadians(this double angle) => Math.PI / 180 * angle;

    internal static double ToDegrees(this double angle) => angle * (180.0 / Math.PI);

    internal static double Normalize(this double angle) => (angle + 360) % 360;
}
