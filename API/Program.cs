using System.Text.Json.Serialization;
using API.Common;
using API.Common.Helpers;
using API.Filters;
using API.Middleware;
using Application;
using infrastructure;
using infrastructure.Persistence;
using Infrastructure.Persistence.Seeders;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
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

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<CookieHelper>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddOpenApi();
{
    var maxRequestBodySize = builder.Configuration
        .GetValue<long>("UploadSettings:MaxRequestBodySizeInMB");
    builder.WebHost.ConfigureKestrel(options => { options.Limits.MaxRequestBodySize = maxRequestBodySize * 1024 * 1024; });
    builder.Services.Configure<FormOptions>(options => { options.MultipartBodyLengthLimit = maxRequestBodySize * 1024 * 1024; });
}

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

app.UseCors("AllowAllOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(mediaPath),
    RequestPath = "/media"
});

app.UseForwardedHeaders();
if (!app.Environment.IsDevelopment())
    app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
    await context.Database.MigrateAsync();
    await AdminSeeder.SeedAsync(context, configuration);
}
//app.UseHttpsRedirection();


app.MapControllers();
app.Run();

