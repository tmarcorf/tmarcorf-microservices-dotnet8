using OpenAI_API;

namespace ChatGPT.ASP.NET.Integration.Extensions
{
    public static class ChatGPTExtensions
    {
        public static WebApplicationBuilder AddChatGPT(
            this WebApplicationBuilder builder)
        {
            var key = Environment.GetEnvironmentVariable("OPENAI_API_KEY");
            var chat = new OpenAIAPI(key);
            builder.Services.AddSingleton(chat);

            return builder;
        }
    }
}
