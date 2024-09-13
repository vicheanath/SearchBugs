using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Shared.Behaviors;
namespace SearchBugs.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(ApplicationAssemblyReference.Assembly);

            config.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
        });

        services.AddValidatorsFromAssembly(ApplicationAssemblyReference.Assembly, includeInternalTypes: true);

        return services;
    }
}
