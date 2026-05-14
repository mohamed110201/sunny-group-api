using Domain.Entities;
using Domain.Entities.Auth;
using infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Seeders;

public static class AdminSeeder
{
    public static async Task SeedAsync(AppDbContext context)
    {
        if (await context.Admins.AnyAsync())
            return;

        var admin = new Admin
        {
            Email = "admin@sunnygroupdevelopers.com",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123456")
        };

        await context.Admins.AddAsync(admin);
        await context.SaveChangesAsync();
    }
}