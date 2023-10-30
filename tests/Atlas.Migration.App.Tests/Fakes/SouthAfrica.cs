// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Fakes;

public sealed record SouthAfrica : ICountryData
{
    public string Common { get; } = "South Africa";

    public string Official { get; } = "Republic of South Africa";

    public string CountryCode3 { get; } = "ZAF";

    public string Region { get; } = "Africa";

    public string FrenchCommon { get; } = "Afrique du Sud";

    public string FrenchOfficial { get; } = "République d'Afrique du Sud";

    public double Latitude { get; } = -29.0;

    public double Longitude { get; } = 24.0;

    public double Area { get; } = 1221037;
}
