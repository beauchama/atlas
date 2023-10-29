// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace Atlas.Infrastructure.Settings.Validations;

internal sealed class FluentValidateOptions<TOptions>(string optionName, IValidator<TOptions> validator) : IValidateOptions<TOptions> where TOptions : class
{
    public ValidateOptionsResult Validate(string? name, TOptions options)
    {
        if (!optionName.Equals(name, StringComparison.Ordinal))
            return ValidateOptionsResult.Skip;

        ValidationResult result = validator.Validate(options);

        return result.IsValid
            ? ValidateOptionsResult.Success
            : ValidateOptionsResult.Fail(result.ToString());
    }
}
