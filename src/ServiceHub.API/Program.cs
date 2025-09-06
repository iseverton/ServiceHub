using Serilog;
using Serilog.Enrichers.CallerInfo;
using Serilog.Events;
using ServiceHub.Application.Handlers.Users;
using ServiceHub.Infrastructure;

// Configuracao do Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Warning)
    .Enrich.WithProperty("App", "ServiceHub")
    .Enrich.WithCallerInfo(
        includeFileInfo: true,
        allowedAssemblies: new List<string> { "ServiceHub.API", "ServiceHub.Application" },
        prefix: "myprefix_")
    .WriteTo.Console(
    outputTemplate: "{Timestamp:HH:mm} [{Level}] ({ThreadId}) {Message} {myprefix_Method} {myprefix_File} {myprefix_LineNumber}{NewLine}{Exception}")

    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    
    builder.Host.UseSerilog();

    // Add services to the container.
    builder.Services.AddControllers();
    // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
    builder.Services.AddOpenApi();

    // injecao de dependencias da infraestrutura
    builder.Services.AddInfrastructure(builder.Configuration);

    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommandHandler).Assembly));

    
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }
    app.UseSerilogRequestLogging();
    app.UseHttpsRedirection();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}



