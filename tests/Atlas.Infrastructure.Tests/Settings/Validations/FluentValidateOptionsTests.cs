// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace Atlas.Infrastructure.Settings.Validations;

public sealed class FluentValidateOptionsTests
{
    private readonly FakeSettings _settings = new("Name");
    private readonly IValidator<FakeSettings> _validator = Substitute.For<IValidator<FakeSettings>>();

    private readonly FluentValidateOptions<FakeSettings> _validation;

    public FluentValidateOptionsTests()
        => _validation = new FluentValidateOptions<FakeSettings>(string.Empty, _validator);

    [Fact]
    public void ValidateShouldSkipWhenGivenNameIsEmpty()
    {
        ValidateOptionsResult result = _validation.Validate("option", _settings);

        result.Should().Be(ValidateOptionsResult.Skip);
    }

    [Fact]
    public void ValidateShouldSkipWhenGivenNameIsNotEmptyAndNotHaveTheSameNamedOption()
    {
        FluentValidateOptions<FakeSettings> validation = new("fake", _validator);

        ValidateOptionsResult result = validation.Validate("option", _settings);

        result.Should().Be(ValidateOptionsResult.Skip);
    }

    [Fact]
    public void ValidateShouldNotSkipWhenGivenNameIsNotEmptyAndHaveTheSameNamedOption()
    {
        FluentValidateOptions<FakeSettings> validation = new("fake", _validator);
        _validator.Validate(_settings).Returns(new ValidationResult());

        ValidateOptionsResult result = validation.Validate("fake", _settings);

        result.Should().NotBe(ValidateOptionsResult.Skip);
    }

    [Fact]
    public void ValidateShouldReturnSuccessWhenValidationIsValid()
    {
        _validator.Validate(_settings).Returns(new ValidationResult());

        ValidateOptionsResult result = _validation.Validate(string.Empty, _settings);

        result.Succeeded.Should().BeTrue();
    }

    [Fact]
    public void ValidateShouldReturnFailWhenValidationIsNotValid()
    {
        ValidationFailure fail = new(nameof(FakeSettings.Name), "Error");
        ValidationResult validationResult = new([fail]);

        _validator.Validate(_settings).Returns(validationResult);

        ValidateOptionsResult result = _validation.Validate(string.Empty, _settings);

        result.Succeeded.Should().BeFalse();
        result.Failed.Should().BeTrue();
        result.FailureMessage.Should().Be(validationResult.ToString());
    }

    public record FakeSettings(string Name);
}
