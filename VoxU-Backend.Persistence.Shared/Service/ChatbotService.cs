using Microsoft.Extensions.Options;
using VoxU_Backend.Core.Application.DTOS.Chatbot;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Persistence.Shared.Options;
using GenerativeAI;


namespace VoxU_Backend.Persistence.Shared.Service
{
    public class ChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClientFactory;
        private GeminiSettings _geminiSettings;
     

        public ChatbotService(HttpClient httpClientFactory, IOptions<GeminiSettings> geminiSettings)
        {
            _httpClientFactory = httpClientFactory;
            _geminiSettings = geminiSettings.Value;
        }


        public async Task<ChatbotResponse> getChatbotReponseAsync(string prompt)
        {
          
            try
            {
                GenerativeModel model = new GenerativeModel(_geminiSettings.API_KEY, "gemini-2.0-flash");
                var result = await model.GenerateContentAsync(prompt);



                return new ChatbotResponse
                {
                    Reponse = result.Text()
                };
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error en la solicitud: " + e.Message);
            }
        }

    }


    }

