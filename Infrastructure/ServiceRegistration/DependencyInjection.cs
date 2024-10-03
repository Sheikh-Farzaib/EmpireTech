using Application.Interfaces;
using Application.Interfaces.IServices;
using Infrastructure.AppSections;
using Infrastructure.Implementations;
using Infrastructure.Implementations.Services;
using System.Data.Common;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = new ConnectionString();
        configuration.GetSection("ConnectionStrings").Bind(connection);
        services.AddSingleton(connection);
        var smtpSettings = new SmtpSettings();
        configuration.GetSection("SmtpSettings").Bind(smtpSettings);
        services.AddSingleton(smtpSettings);
        services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        services.AddTransient<IEmailNotificationRepository, EmailNotificationRepository>();


        return services;
    }
}
