using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;using RafaelaColabora.Models;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RafaelaColabora.Data;

[assembly: HostingStartup(typeof(RafaelaColabora.Areas.Identity.IdentityHostingStartup))]
namespace RafaelaColabora.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}