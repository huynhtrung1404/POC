using Microsoft.EntityFrameworkCore;
using Poc.Infra.Context;

namespace Poc.Api.Extensions;

public static class MiddlewareExtension
{
    public static IApplicationBuilder UseMigration(this IApplicationBuilder applicationBuilder)
    {
        using var scope = applicationBuilder.ApplicationServices.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<PocContext>();
        try
        {
            context.Database.Migrate(); // Applies any pending migrations
        }
        catch (Exception ex)
        {
            // Log the error (use your logger)
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while migrating the database.");
        }
        // You can now use the serviceProvider to resolve services
        return applicationBuilder;
    }
}