using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using OpenAI_API.Models;

namespace OpenAIIntegration.Controllers
{
    [Route("bot/[controller]")]
    public class ChatGPTController : ControllerBase
    {
        private readonly OpenAIAPI _chatGpt;

        public ChatGPTController(OpenAIAPI chatGpt)
        {
            _chatGpt = chatGpt;
        }

        [HttpGet()]
        public async Task<IActionResult> Chat([FromQuery(Name = "prompt")] string prompt)
        {
            var response = "";

            var completion = new CompletionRequest
            {
                Prompt = prompt,
                Model = Model.ChatGPTTurbo,
                MaxTokens = 200
            };

            var result = await _chatGpt.Completions.CreateCompletionAsync(completion);
            result.Completions.ForEach(resultText => response = resultText.Text);

            return Ok(response);
        }
    }
}
