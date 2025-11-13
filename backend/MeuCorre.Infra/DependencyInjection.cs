using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra.Data.Context;
using MeuCorre.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeuCorre.Infra
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services, IConfiguration configuration)
        {
            //Busca a string de conexão no arquivo appsettings.json
            var connectionString = configuration.GetConnectionString("Mysql");

            //Registra o MeuDbContext e configura o uso do MySQL
            services.AddDbContext<MeuDbContext>(options => 
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            //Registra os repositorios para eles funcionarem com injeção de dependência
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            return services;
        }
    }
}
