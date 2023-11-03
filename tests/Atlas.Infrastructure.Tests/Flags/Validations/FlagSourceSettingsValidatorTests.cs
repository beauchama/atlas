// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure.Flags.Settings;
using Atlas.Infrastructure.Flags.Settings.Validations;
using FluentValidation.TestHelper;

namespace Atlas.Infrastructure.Flags.Validations;

public sealed class FlagSourceSettingsValidatorTests
{
    private readonly FlagSourceSettings _settings = new()
    {
        Endpoint = new Uri("https://flags.com")
    };

    private readonly FlagSourceSettingsValidator _validator = new();

    [Fact]
    public void ValidateShouldNotHaveErrorForCountryEndpointWhenAllRulesAreValid()
    {
        TestValidationResult<FlagSourceSettings> result = _validator.TestValidate(_settings);

        result.ShouldNotHaveValidationErrorFor(x => x.Endpoint);
    }

    [Fact]
    public void ValidateShouldHaveErrorForCountryEndpointWhenIsEmpty()
    {
        TestValidationResult<FlagSourceSettings> result = _validator.TestValidate(_settings with { Endpoint = default! });

        result.ShouldHaveValidationErrorFor(x => x.Endpoint)
            .WithErrorMessage("'Endpoint' must not be empty.");
    }

    [Theory, ClassData(typeof(Schemes))]
    public void ValidateShouldHaveErrorForCountryEndpointWhenIsSchemeIsNotHttps(string scheme)
    {
        TestValidationResult<FlagSourceSettings> result = _validator.TestValidate(_settings with
        {
            Endpoint = new Uri($"{scheme}://hello-world.com")
        });

        result.ShouldHaveValidationErrorFor(x => x.Endpoint).WithErrorMessage("'Endpoint' must be https.");
    }
}

file sealed class Schemes : TheoryData<string>
{
    public Schemes()
    {
        Add(Uri.UriSchemeFile);
        Add(Uri.UriSchemeFtp);
        Add(Uri.UriSchemeFtps);
        Add(Uri.UriSchemeGopher);
        Add(Uri.UriSchemeHttp);
        Add(Uri.UriSchemeMailto);
        Add(Uri.UriSchemeNetPipe);
        Add(Uri.UriSchemeNetTcp);
        Add(Uri.UriSchemeNews);
        Add(Uri.UriSchemeNntp);
        Add(Uri.UriSchemeSftp);
        Add(Uri.UriSchemeSsh);
        Add(Uri.UriSchemeTelnet);
        Add(Uri.UriSchemeWs);
        Add(Uri.UriSchemeWss);
    }
}
