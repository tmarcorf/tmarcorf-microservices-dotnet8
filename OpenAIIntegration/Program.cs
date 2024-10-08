
using ChatGPT.ASP.NET.Integration.Extensions;

namespace OpenAIIntegration
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var appName = "ChatGPT ASP.NET 8 Integration";

            builder.AddChatGPT();
            builder.AddSerilog(builder.Configuration, appName);

            // Add services to the container.

            builder.Services.AddRouting(options => options.LowercaseUrls = true);
            builder.Services.AddControllers();
            builder.Services.AddSwagger(builder.Configuration, appName);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            var app = builder.Build();

            app.UseSwaggerDoc(appName);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
