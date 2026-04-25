using System.Text.Json.Serialization;
using API.Filters;
using API.Middleware;
using Application;
using infrastructure;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
    {
        options.Filters.Add<ValidationFilter>();
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    });

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddOpenApi();
var app = builder.Build();

 if (app.Environment.IsDevelopment())
 {
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "Sunny Group API ");
    });
}
 var mediaPath = builder.Configuration["MediaStorage:BasePath"]!;

 if (!Directory.Exists(mediaPath))
     Directory.CreateDirectory(mediaPath);

 app.UseStaticFiles(new StaticFileOptions
 {
     FileProvider = new PhysicalFileProvider(mediaPath),
     RequestPath = "/media"
 });

app.UseForwardedHeaders();
if (!app.Environment.IsDevelopment())
    app.UseMiddleware<ExceptionHandlingMiddleware>();

//app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", (IConfiguration config) =>
    {
        var connectionString = config.GetConnectionString("Default");
        var environment = app.Environment.EnvironmentName;

        return Results.Ok(new
        {
            Environment = environment,
            ConnectionString = connectionString
        });
    })
    .WithName("GetWeatherForecast");
app.MapControllers();
app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}