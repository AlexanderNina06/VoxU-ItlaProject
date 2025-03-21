using Microsoft.AspNetCore.Mvc;
using VoxU_Backend.Core.Application.DTOS.Chatbot;
using VoxU_Backend.Core.Application.Interfaces.Services;

namespace VoxU_Backend.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly IChatbotService _chatbotService;
        public ChatbotController(IChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        [HttpPost]
        public async Task<IActionResult> ApiResponse([FromBody] string prompt)
        {
            if (prompt is null)
            {
                return BadRequest();
            }

            ChatbotResponse response = new();
            try
            {
                response = await _chatbotService.getChatbotReponseAsync(prompt);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }


          return Ok(response);
        }

        [HttpGet("Questions")]
        public IActionResult getQuestions()
        {
            return Ok(new QuestionsResponse());
        }

    }
}
