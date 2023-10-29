// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using Atlas.Infrastructure.Settings.Validations;
using FluentValidation;

namespace Atlas.Infrastructure.Flags.Settings.Validations;

internal sealed class FlagSourceSettingsValidator : AbstractValidator<FlagSourceSettings>
{
    public FlagSourceSettingsValidator()
    {
        _ = RuleFor(x => x.Endpoint)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Https()
            .WithMessage(f => $"'{nameof(f.Endpoint)}' must be https.");
    }
}
