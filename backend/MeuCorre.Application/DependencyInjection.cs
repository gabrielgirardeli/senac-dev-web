
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MeuCorre.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)

         {
           services.AddMediatR(m =>
           m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
                
            return services;
        }
    }

}