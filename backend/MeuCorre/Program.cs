
using MeuCorre.Application;
using MeuCorre.Domain.Interfaces.Repositories;
using MeuCorre.Infra;
using MeuCorre.Infra.Repositories;

namespace MeuCorre
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddInfra(builder.Configuration);
            builder.Services.AddApplication(builder.Configuration);

            //// esse 
            builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
