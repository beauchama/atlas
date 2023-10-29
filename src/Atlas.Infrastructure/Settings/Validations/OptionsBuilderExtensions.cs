// Copyright (c) Alexandre Beauchamp. All rights reserved.
// The source code is licensed under MIT License.

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;

namespace Atlas.Infrastructure.Settings.Validations;

[ExcludeFromCodeCoverage]
public static class OptionsBuilderExtensions
{
    public static IServiceCollection AddFluentOptions<TOptions, TValidator>(this IServiceCollection services, string? name, string configSectionPath)
        where TOptions : class
        where TValidator : class, IValidator<TOptions>
    {
        _ = services.AddOptions<TOptions>(name)
            .BindConfiguration(configSectionPath)
            .AddFluentValidations<TOptions, TValidator>()
            .ValidateOnStart();

        return services;
    }

    private static OptionsBuilder<TOptions> AddFluentValidations<TOptions, TValidator>(this OptionsBuilder<TOptions> builder)
        where TOptions : class
        where TValidator : class, IValidator<TOptions>
    {
        builder.Services.TryAddTransient<IValidator<TOptions>, TValidator>();
        _ = builder.Services.AddTransient<IValidateOptions<TOptions>>(provider => InjectFluentValidateOptions(provider, builder.Name));

        return builder;

        static FluentValidateOptions<TOptions> InjectFluentValidateOptions(IServiceProvider provider, string optionName)
        {
            IValidator<TOptions> validator = provider.GetRequiredService<IValidator<TOptions>>();
            return new FluentValidateOptions<TOptions>(optionName, validator);
        }
    }
}
