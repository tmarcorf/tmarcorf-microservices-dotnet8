using AutoMapper;
using GeekShopping.ProductAPI.Config;
using GeekShopping.ProductAPI.Model.Context;
using GeekShopping.ProductAPI.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace GeekShopping.ProductAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var services = builder.Services;

            var connection = builder.Configuration["MySQLConnection:MySQLConnectionString"];

            services.AddDbContext<MySQLContext>(options =>
            {
                options.UseMySql(connection, new MySqlServerVersion(new Version(8, 3)));
            });

            IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();

            services.AddSingleton(mapper);
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IProductRepository, ProductRepository>();

            // Add services to the container.
            services.AddControllers();
            
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GeekShopping.ProductAPI" });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
