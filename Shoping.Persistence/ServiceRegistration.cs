using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Audit.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Microsoft.AspNetCore.Identity;
using Shoping.Persistence.Repositories;

namespace Shoping.Persistence
{
    public static class ServiceRegistration
    {

        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                if (configuration.GetValue<bool>("UseInMemory"))
                {
                    options.UseInMemoryDatabase(nameof(AppDbContext));
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("Default"));
                }
            });

                 services
               .AddIdentityCore<User>()
               .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>();
 

            //Audit.Core.Configuration.Setup()
            //    .UseAzureStorageBlobs(config => config
            //        .WithConnectionString(configuration["AuditLogs:ConnectionString"])
            //        .ContainerName(ev => $"mediatrlogs{DateTime.Today:yyyyMMdd}")
            //        .BlobName(ev =>
            //        {
            //            var currentUser = ev.CustomFields["User"] as CurrentUser;

            //            return $"{ev.EventType}/{currentUser?.Id}_{DateTime.UtcNow.Ticks}.json";
            //        })
            //    );


                services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

    }
}
