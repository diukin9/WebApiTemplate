﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template.Data.EntityFrameworkCore;
using Template.Domain.RelationalDatabase.Entities;
using Template.Infrastructure.Common.AppDefinition;
using Template.Infrastructure.Common.Attributes;

namespace Template.Application.Definitions;

[CallingOrder(1)]
public class DatabasesDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(config =>
        {
            config.UseNpgsql(configuration["ConnectionStrings:PostgreSQL"]);
            config.EnableSensitiveDataLogging();
        });

        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(config =>
        {
            config.Password.RequireDigit = false;
            config.Password.RequireLowercase = false;
            config.Password.RequireNonAlphanumeric = false;
            config.Password.RequireUppercase = false;
            config.Password.RequiredLength = 8;
        }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

        services.AddTransient<IdentityErrorDescriber>();
    }
}
