// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Domain.Geography;

public class DirectionTests
{
    public static TheoryData<GeographicCoordinate, GeographicCoordinate, double> Coordinates { get; } = new TheoryData<GeographicCoordinate, GeographicCoordinate, double>()
    {
        { new GeographicCoordinate(35, 105), new GeographicCoordinate(36, 138), 87.0 }, // China to Japan
        { new GeographicCoordinate(60, -95), new GeographicCoordinate(36, 138), 253.0 }, // Canada to Japan
        { new GeographicCoordinate(60, -95), new GeographicCoordinate(38, -97), 183.0 }, // Canada to USA
        { new GeographicCoordinate(60, -95), new GeographicCoordinate(42.83333333, 12.83333333), 104.0 }, // Canada to Italy
        { new GeographicCoordinate(42.83333333, 12.83333333), new GeographicCoordinate(60, -95), 284.0 }, // Italy to Canada
        { new GeographicCoordinate(42.83333333, 12.83333333), new GeographicCoordinate(36, 138), 94.0 }, // Italy to Japan
        { new GeographicCoordinate(42.83333333, 12.83333333), new GeographicCoordinate(28, 3), 208.0 }, // Italy to Algeria
        { new GeographicCoordinate(60, -95), new GeographicCoordinate(-18, 175), 223.0 }, // Canada to Fiji
        { new GeographicCoordinate(42.83333333, 12.83333333), new GeographicCoordinate(-18, 175), 112.0 }, // Italy to Fiji
        { new GeographicCoordinate(-18, 175), new GeographicCoordinate(60, -95), 43.0 }, // Fiji to Canada
        { new GeographicCoordinate(-18, 175), new GeographicCoordinate(42.83333333, 12.83333333), 292.0 }, // Fiji to Italy
    };

    [Theory]
    [MemberData(nameof(Coordinates))]
    public void DirectionShouldCalculateTheAngleWithTheCoordinates(GeographicCoordinate from, GeographicCoordinate to, double expectedDirection)
    {
        Direction direction = Direction.Calculate(from, to);

        direction.ToDouble().Should().Be(expectedDirection);
    }

    [Fact]
    public void DirectionShouldReturnZeroWhenCoordinatesAreSame()
    {
        double direction = Direction.Calculate(new GeographicCoordinate(60, -95), new GeographicCoordinate(60, -95));

        direction.Should().Be(0.0);
    }

    [Fact]
    public void ImplicitConversionShouldReturnTheAngle()
    {
        double direction = Direction.Calculate(new GeographicCoordinate(0, 0), new GeographicCoordinate(0, 0));

        direction.Should().Be(0.0);
    }
}
