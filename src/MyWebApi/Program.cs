// <copyright file="Program.cs" company="Ken Wong">
// Copyright (c) Ken Wong.  All rights reserved.
// See LICENSE file in the project root for full license information.
// </copyright>

using MyWebApi.Extensions;

using var loggerFactory = LoggerFactory.Create(config => config.AddConsole());
var logger = loggerFactory.CreateLogger("Program");
var name = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name ?? "Application";

try
{
    logger.LogApplicationStarting(name);

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    var summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching",
    };

    app.MapGet(
        "/weatherforecast",
        () =>
        {
            return Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast(
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    55,
                    summaries[index]))
                .ToArray();
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();
    app.Run();
}
#pragma warning disable CA1031 // Do not catch general exception types
catch (Exception ex)
#pragma warning restore CA1031 // Do not catch general exception types
{
    logger.LogCriticalError(name, ex);
    Environment.Exit(1);
}
finally
{
    logger.LogApplicationStopped(name);
}

#pragma warning disable SA1600 // Elements should be documented
#pragma warning disable SA1649 // File name should match first type name
#pragma warning disable SA1402 // File may only contain a single type
internal sealed record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
#pragma warning restore SA1402 // File may only contain a single type
{
    public int TemperatureF => 32 + (int)(this.TemperatureC / 0.5556);
}
#pragma warning restore SA1649 // File name should match first type name
#pragma warning restore SA1600 // Elements should be documented
