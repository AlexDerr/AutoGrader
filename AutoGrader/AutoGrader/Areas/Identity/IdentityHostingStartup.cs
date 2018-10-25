using System;
using AutoGrader.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(AutoGrader.Areas.Identity.IdentityHostingStartup))]
namespace AutoGrader.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddEntityFrameworkNpgsql().AddDbContext<AuthenticationContext>(options =>
                    options.UseNpgsql(
                        context.Configuration.GetConnectionString("Default")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<AuthenticationContext>();
            });
        }
    }
}