using Aggregator.Application.IoC;
using Aggregator.Infrastructure.DataAccess;
using Aggregator.Infrastructure.IoC;
using FastEndpoints;
using FastEndpoints.Swagger;

var builder = WebApplication.CreateBuilder();

builder.Services
    .AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "Aggregator.API";
            s.Version = "v1";
        };
    });

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
      .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);

builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseFastEndpoints(c =>
    {
        c.Endpoints.RoutePrefix = "api";
    })
    .UseSwaggerGen();

app.Services.MigrateDatabase<UnitOfWork>();

app.Services.InitializeData<UnitOfWork>();

app.Run();