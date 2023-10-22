// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Extensions;

namespace Atlas.Domain.Geography;

public sealed record Direction
{
    private readonly double _angle;

    private Direction(double angle) => _angle = angle;

    public static implicit operator double(Direction direction) => direction.ToDouble();

    /// <summary>
    /// Calculate the direction based on Rhumb line formula.
    /// https://www.movable-type.co.uk/scripts/latlong.html.
    /// </summary>
    /// <param name="from">The coordinate of from.</param>
    /// <param name="to">The coordinate of to.</param>
    /// <returns>The normalized direction to be within [0, 360] degrees.</returns>
    internal static Direction Calculate(GeographicCoordinate from, GeographicCoordinate to)
    {
        if (from == to)
            return new(0.0);

        double deltaLatitude = CalculateDeltaLatitude(from.Latitude, to.Latitude);
        double deltaLongitude = CalculateDeltaLongitude(from.Longitude, to.Longitude);

        double bearing = Math.Floor(Math.Atan2(deltaLongitude, deltaLatitude).ToDegrees());

        return new(bearing.Normalize());
    }

    /// <summary>
    /// Calculate Δ latitude.
    /// In the calculation of the rhumb line, the use of Math.PI / 4 is related to the Mercator projection,
    /// which is used to convert between geographic latitude (in degrees) and the Mercator projection's auxiliary latitude.
    /// </summary>
    /// <param name="fromLatitude">from latitude.</param>
    /// <param name="toLatitude">to latitude.</param>
    /// <returns>The calculated Δ latitude.</returns>
    private static double CalculateDeltaLatitude(double fromLatitude, double toLatitude)
    {
        const double auxiliaryLatitude = Math.PI / 4;

        double fromTangent = Math.Tan(auxiliaryLatitude + (fromLatitude.ToRadians() / 2));
        double toTangent = Math.Tan(auxiliaryLatitude + (toLatitude.ToRadians() / 2));

        return Math.Log(toTangent / fromTangent);
    }

    /// <summary>
    /// Calculate Δ longitude.
    /// If Δ longitude is over 180°, take the shorter rhumb line across the anti-meridian.
    /// </summary>
    /// <param name="fromLongitude">from longitude.</param>
    /// <param name="toLongitude">to longitude.</param>
    /// <returns>the calculated Δ longitude.</returns>
    private static double CalculateDeltaLongitude(double fromLongitude, double toLongitude)
    {
        double deltaLongitude = (toLongitude - fromLongitude).ToRadians();

        return Math.Abs(deltaLongitude) > Math.PI
            ? GetShorterRhumbLine(deltaLongitude)
            : deltaLongitude;

        static double GetShorterRhumbLine(double deltaLongitude)
        {
            return deltaLongitude > 0
                ? -((2 * Math.PI) - deltaLongitude)
                : (2 * Math.PI) + deltaLongitude;
        }
    }

    public double ToDouble() => _angle;
}
