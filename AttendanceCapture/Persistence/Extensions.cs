using AttendanceCapture.Infrastructure;
using AttendanceCapture.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AttendanceCapture.Persistence;

public static class Extensions
{
    public static IServiceCollection RegisterPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<UniversityContext>(option =>
        {
            option.UseNpgsql(configuration.GetConnectionString("MyDB"));

        });


        return services;
    }

    public static IServiceCollection RegisterIdentity(this IServiceCollection services)
    {
        services.AddIdentity<User, ApplicationRole>(options =>
        {
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 6;
            options.Lockout.AllowedForNewUsers = true;

            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredUniqueChars = 1;
            options.SignIn.RequireConfirmedEmail = true;
        })
        .AddRoles<ApplicationRole>()
        .AddEntityFrameworkStores<UniversityContext>()
        .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(options => options.TokenLifespan = TimeSpan.FromMinutes(15));

        return services;
    }

    public static IServiceCollection RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = "shepherd",
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(configuration["JwtSettings:Secret"]!)),
                ValidateIssuer = false,
                ValidateAudience = true,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.FromSeconds(0),
                ValidateIssuerSigningKey = true
            };
        });

       

        return services;
    }


    public static void seed (this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User
            {
                AccountStatus = AccountStatusEnum.Active,
                ConcurrencyStamp = Guid.NewGuid().ToString("D"),
                Email = "technology@facifier.com",
                EmailConfirmed = true,
                FirstName = "FInalYear",
                Id = 1,
                LastName = "Administrator",
                NormalizedEmail = "TECHNOLOGY@FACIFIER.COM",
                NormalizedUserName = "TECHNOLOGY@FACIFIER.COM",
                PasswordHash = new PasswordHasher<User>().HashPassword(null!, "password"),
                SecurityStamp = GenerateSecurityStamp(),
                TimeCreated = DateTimeOffset.UtcNow,
                TimeUpdated = DateTimeOffset.UtcNow,
                UserName = "FACIFIER",
                UserType = UserType.Admin
            }
        );
    }
    private static readonly Func<string> GenerateSecurityStamp = () =>
    {
        var guid = Guid.NewGuid();
        return string.Concat(Array.ConvertAll(guid.ToByteArray(), b => b.ToString("X2")));
    };
}
