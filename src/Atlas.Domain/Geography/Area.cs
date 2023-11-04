// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Domain.Geography;

public sealed record Area
{
    private readonly double _area;

    public Area(double area) => _area = area;

    public static implicit operator double(Area area) => area.ToDouble();

    public AreaSize CompareTo(Area other)
    {
        if (_area == other)
            return AreaSize.Same;

        return _area > other
            ? AreaSize.Larger
            : AreaSize.Smaller;
    }

    public double ToDouble() => _area;
}
