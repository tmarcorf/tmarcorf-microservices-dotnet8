using Microsoft.AspNetCore.Mvc;
using OpenAI;

namespace OpenAIIntegration.Controllers
{
    [Route("bot/[controller]")]
    public class ChatGPTController : ControllerBase
    {
        private readonly OpenAIClient _chatGpt;

        public ChatGPTController(OpenAIClient chatGpt)
        {
            _chatGpt = chatGpt;
        }

        [HttpGet()]
        public async Task<IActionResult> Chat([FromQuery(Name = "prompt")] string prompt)
        {
            var reponse = "";

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
