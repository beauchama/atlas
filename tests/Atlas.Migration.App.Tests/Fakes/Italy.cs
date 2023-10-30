// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Fakes;

public sealed record Italy : ICountryData
{
    public string Common { get; } = "Italy";

    public string Official { get; } = "Italian Republic";

    public string CountryCode3 { get; } = "ITA";

    public string Region { get; } = "Europe";

    public string FrenchCommon { get; } = "Italie";

    public string FrenchOfficial { get; } = "République italienne";

    public double Latitude { get; } = 42.83333333;

    public double Longitude { get; } = 12.83333333;

    public double Area { get; } = 301336;
}
