// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Fakes;

public sealed record Antarctica : ICountryData
{
    public string Common { get; } = "Antarctica";

    public string Official { get; } = "Antarctica";

    public string CountryCode3 { get; } = "ATA";

    public string Region { get; } = "Antarctic";

    public string FrenchCommon { get; } = "Antarctique";

    public string FrenchOfficial { get; } = "Antarctique";

    public double Latitude { get; } = -90;

    public double Longitude { get; }

    public double Area { get; } = 14000000;
}
