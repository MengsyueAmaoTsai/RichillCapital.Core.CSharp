using FluentValidation;

using Microsoft.Extensions.DependencyInjection;

using RichillCapital.Core.SharedKernel;

namespace RichillCapital.Core.Features;

public static class DependencyInjection
{
    public static IServiceCollection AddFeatures(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddScoped<IDomainEventDispatcher, MediatRDomainEventDispatcher>();

        services.AddValidatorsFromAssembly(assembly, includeInternalTypes: true);

        return services;
    }
}