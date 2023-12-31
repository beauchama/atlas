// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using FluentValidation;

namespace Atlas.Infrastructure.Settings.Validations;

public static class FluentValidationsExtensions
{
    public static IRuleBuilderOptions<T, Uri> Https<T>(this IRuleBuilder<T, Uri> builder)
        => builder.Must(uri => uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.Ordinal));
}
