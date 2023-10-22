// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Extensions;

namespace Atlas.Domain.Geography;

public sealed record Distance
{
    private Distance(double length, DistanceUnit unit)
    {
        Length = length;
        Unit = unit;
    }

    public double Length { get; }

    public DistanceUnit Unit { get; }

    /// <summary>
    /// Calculate the distance between two coordinates based on Haversine formula.
    /// https://www.movable-type.co.uk/scripts/latlong.html.
    /// </summary>
    /// <param name="from">the coordinate of from.</param>
    /// <param name="to">the coordinate of to.</param>
    /// <param name="unit">the unit of the distance.</param>
    /// <returns>The distance between two coordinates in the specified unit.</returns>
    internal static Distance Calculate(GeographicCoordinate from, GeographicCoordinate to, DistanceUnit unit)
    {
        double earthRadius = GetEarthRadius(unit);

        double deltaLatitude = (to.Latitude - from.Latitude).ToRadians();
        double deltaLongitude = (to.Longitude - from.Longitude).ToRadians();
        double fromLatitude = from.Latitude.ToRadians();
        double toLatitude = to.Latitude.ToRadians();

        double sinLatitude = Math.Sin(deltaLatitude / 2);
        double sinLongitude = Math.Sin(deltaLongitude / 2);

        double a = (sinLatitude * sinLatitude) + (sinLongitude * sinLongitude * Math.Cos(fromLatitude) * Math.Cos(toLatitude));
        double c = 2 * Math.Asin(Math.Sqrt(a));

        return new(earthRadius * c, unit);
    }

    private static double GetEarthRadius(DistanceUnit unit)
    {
        const double earthRadiusInMiles = 3958.8;
        const double earthRadiusInKilometers = 6371.0;

        return unit == DistanceUnit.Kilometers
            ? earthRadiusInKilometers
            : earthRadiusInMiles;
    }
}
