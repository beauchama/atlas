// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Domain.Geography;
using Atlas.Migration.App.Geography.Dto;

namespace Atlas.Migration.App.Geography.Mappers;

internal static class GeographicCoordinateDtoMapper
{
    internal static GeographicCoordinate AsDomain(this GeographicCoordinateDto coordinate)
        => new(coordinate.Latitude, coordinate.Longitude);
}
