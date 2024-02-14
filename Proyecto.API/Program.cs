using Microsoft.EntityFrameworkCore;
using Proyecto.BLL.Services;
using Proyecto.DAL.Data;
using Proyecto.DAL.Repositories;
using Proyecto.Models.Models;

namespace Proyecto.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(opciones =>
            {
                opciones.UseSqlServer(builder.Configuration.GetConnectionString("Connection"));
            });

            //INYECCION DE DEPENDENCIAS
            builder.Services.AddScoped<IGenericRepository<Vehiculo>, VehiculoRepository>();
            builder.Services.AddScoped<IVehiculoService, VehiculoService>();


            builder.Services.AddControllers();
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
