using Std.API;
using Std.API.Emplyees.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.InjectServices(builder.Configuration);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.SeedDatabase();

var service = app.Services.GetRequiredService<EmployeeChangeNotifier>();
service.Subscribe();

app.Run();