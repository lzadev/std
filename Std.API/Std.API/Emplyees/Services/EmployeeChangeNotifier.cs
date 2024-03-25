using AutoMapper;
using Std.API.Emplyees.DTOs;
using Std.API.Emplyees.Interfaces;
using Std.API.Models;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;

namespace Std.API.Emplyees.Services;

public class EmployeeChangeNotifier(IConfiguration configuration, IMapper mapper, IServiceProvider serviceProvider) : IDisposable
{
    private readonly string _connectionString = configuration.GetConnectionString("maindb") ?? "";
    private SqlTableDependency<Employee>? _notifier;

    public void Subscribe()
    {
        _notifier = new SqlTableDependency<Employee>(_connectionString, "Employees");
        _notifier.OnChanged += this.TableDependency_Changed;
        _notifier.Start();
    }

    private void TableDependency_Changed(object sender, RecordChangedEventArgs<Employee> e)
    {
        using (var scope = serviceProvider.CreateScope())
        {
            var service = scope.ServiceProvider.GetRequiredService<IBackUpEmployeeService>();

            switch (e.ChangeType)
            {
                case ChangeType.Delete:
                case ChangeType.Insert:
                case ChangeType.Update:
                    service.CreateOrUpdate(mapper.Map<CreateOrUpdateEmployeeBackUpDTO>(e.Entity));
                    break;
            }

        }

        Console.WriteLine(e.Entity.ToString());
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
