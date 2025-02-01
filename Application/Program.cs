using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;
using Web.Api;
using Web.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, loggerConfig) => loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.Services.AddSwaggerGenWithAuth();

builder.Services.AddApplication();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
