using Microsoft.Extensions.Options;
using Org.BouncyCastle.Asn1.Cmp;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using VoxU_Backend.Core.Application.DTOS.Chatbot;
using VoxU_Backend.Core.Application.Interfaces.Services;
using VoxU_Backend.Persistence.Shared.Options;


namespace VoxU_Backend.Persistence.Shared.Service
{
    public class ChatbotService : IChatbotService
    {
        private readonly HttpClient _httpClientFactory;
        private DeepSeekSettings _deepSeekSettings;
     

        public ChatbotService(HttpClient httpClientFactory, IOptions<DeepSeekSettings> deepSeekSettings)
        {
            _httpClientFactory = httpClientFactory;
            _deepSeekSettings = deepSeekSettings.Value;
        }


        public async Task<ChatbotResponse> getChatbotReponseAsync(string prompt)
        {


            var jsonPayload = JsonSerializer.Serialize(prompt);
            var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

            _httpClientFactory.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _deepSeekSettings.API_KEY);

            try
            {
                var response = await _httpClientFactory.PostAsync(_deepSeekSettings.URL, httpContent);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<JsonElement>(responseContent);

                return new ChatbotResponse
                {
                    Reponse = responseJson.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString()
                };
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error en la solicitud: " + e.Message);
            }
        }

    }


    }

