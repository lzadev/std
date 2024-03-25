using Microsoft.EntityFrameworkCore;
using Std.API.Context;
using Std.API.Emplyees.Interfaces;
using Std.API.Emplyees.Repository;
using Std.API.Emplyees.Services;
using Std.API.Models;
using Std.API.SystemParameters.Interfaces;
using Std.API.SystemParameters.Repository;

namespace Std.API;

public static class ServicesExtension
{
    public static void InjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<MainApplicationDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("maindb"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                }
            ));

        services.AddDbContext<BackUpApplicationContext>(x => x.UseSqlServer(configuration.GetConnectionString("backupdb"),
        sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 3,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null
                    );
                }
            ));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddTransient<IEmployeeService, EmplyeeService>();
        services.AddTransient<ISystemParameterRepository, SystemParameterRepository>();
        services.AddTransient<IEmployeeRepository, MainEmployeeRepository>();
        services.AddTransient<IBackUpEmployeeRepository, BackUpEmployeeRespository>();
        services.AddTransient<IBackUpEmployeeService, BackUpEmployeeService>();
        services.AddSingleton<EmployeeChangeNotifier>();
    }

    public static void SeedDatabase(this WebApplication webApplication)
    {
        using var scope = webApplication.Services.CreateScope();
        using var dbContext = scope.ServiceProvider.GetRequiredService<MainApplicationDbContext>();
        try
        {
            if (!dbContext.SystemParameters.Any())
            {
                List<SystemParameter> systemParameters = new()
                {
                   new SystemParameter
                   {
                       Prefix = "EMP",
                       Sequence = "000000",
                       Name = "EMPLOYEE-CODE"
                   }
                };

                dbContext.AddRange(systemParameters);
                dbContext.SaveChanges();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
