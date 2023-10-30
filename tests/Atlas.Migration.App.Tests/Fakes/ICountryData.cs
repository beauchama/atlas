// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

namespace Atlas.Migration.App.Fakes;

public interface ICountryData
{
    string Common { get; }

    string Official { get; }

    string CountryCode3 { get; }

    string Region { get; }

    string FrenchCommon { get; }

    string FrenchOfficial { get; }

    double Latitude { get; }

    double Longitude { get; }

    double Area { get; }
}
