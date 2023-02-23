using Shoping.Application.Common.Services;
using MediatrExample.ApplicationCore.Infrastructure.Services.AzureQueues;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Infrastructure
{
    public static class ServiceExtention
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IQueuesService, AzureStorageQueueService>();

            return services;
        }

    }
}
