using Domain.Entities;
using Domain.Entities.Auth;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Persistence.Seeders;

public static class AdminSeeder
{
    public static async Task SeedAsync(AppDbContext context, IConfiguration configuration)
    {
        var adminsConfig = configuration
            .GetSection("AdminSeed:Admins")
            .Get<List<AdminSeedConfig>>();

        if (adminsConfig is null || adminsConfig.Count == 0)
            return;

        foreach (var adminConfig in adminsConfig)
        {
            var exists = await context.Admins
                .AnyAsync(a => a.Email == adminConfig.Email);

            if (exists) continue;

            await context.Admins.AddAsync(new Admin
            {
                Email = adminConfig.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(adminConfig.Password)
            });
        }

        await context.SaveChangesAsync();
    }
}

public class AdminSeedConfig
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}