// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Domain.Distances;

public sealed record Distance
{
    private Distance(double length, DistanceUnit unit)
    {
        Length = length;
        Unit = unit;
    }

    public double Length { get; }

    public DistanceUnit Unit { get; }

    public static Distance Calculate(GeographicCoordinate from, GeographicCoordinate to, DistanceUnit unit)
    {
        double earthRadius = GetEarthRadius(unit);

        double deltaLatitude = ToRadians(to.Latitude - from.Latitude);
        double deltaLongitude = ToRadians(to.Longitude - from.Longitude);
        double fromLatitude = ToRadians(from.Latitude);
        double toLatitude = ToRadians(to.Latitude);

        double sinLatitude = Math.Sin(deltaLatitude / 2);
        double sinLongitude = Math.Sin(deltaLongitude / 2);

        double a = (sinLatitude * sinLatitude) + (sinLongitude * sinLongitude * Math.Cos(fromLatitude) * Math.Cos(toLatitude));
        double c = 2 * Math.Asin(Math.Sqrt(a));

        return new(earthRadius * c, unit);

        static double ToRadians(double degrees) => Math.PI * degrees / 180.0;
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
