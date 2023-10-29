// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure.Flags.Settings;
using Atlas.Infrastructure.Flags.Settings.Validations;
using FluentValidation.TestHelper;

namespace Atlas.Infrastructure.Flags.Validations;

public class FlagSourceSettingsValidatorTests
{
    private readonly FlagSourceSettings _settings = new()
    {
        Endpoint = new Uri("https://flags.com")
    };

    private readonly FlagSourceSettingsValidator _validator = new();

    public static TheoryData<string> Schemes { get; } = new()
    {
        { Uri.UriSchemeFile },
        { Uri.UriSchemeFtp },
        { Uri.UriSchemeFtps },
        { Uri.UriSchemeGopher },
        { Uri.UriSchemeHttp },
        { Uri.UriSchemeMailto },
        { Uri.UriSchemeNetPipe },
        { Uri.UriSchemeNetTcp },
        { Uri.UriSchemeNews },
        { Uri.UriSchemeNntp },
        { Uri.UriSchemeSftp },
        { Uri.UriSchemeSsh },
        { Uri.UriSchemeTelnet },
        { Uri.UriSchemeWs },
        { Uri.UriSchemeWss },
    };

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

    [Theory]
    [MemberData(nameof(Schemes))]
    public void ValidateShouldHaveErrorForCountryEndpointWhenIsSchemeIsNotHttps(string scheme)
    {
        TestValidationResult<FlagSourceSettings> result = _validator.TestValidate(_settings with
        {
            Endpoint = new Uri($"{scheme}://hello-world.com")
        });

        result.ShouldHaveValidationErrorFor(x => x.Endpoint).WithErrorMessage("'Endpoint' must be https.");
    }
}
