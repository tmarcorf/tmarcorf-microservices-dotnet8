using Microsoft.OpenApi.Models;

namespace ChatGPT.ASP.NET.Integration.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwagger(
            this IServiceCollection services, 
            IConfiguration configuration,
            string appName)
        {
            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = appName,
                    Version = "v1",
                });

                config.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });
        }
        
        public static void UseSwaggerDoc(this IApplicationBuilder app, string appName)
        {
            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", appName);
                config.RoutePrefix = "swagger";
            });
        }
    }
}
