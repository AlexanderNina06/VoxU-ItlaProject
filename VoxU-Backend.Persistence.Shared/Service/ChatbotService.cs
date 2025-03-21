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
                // Agregar contexto de la app y módulos disponibles antes del prompt del usuario.
                string appContext = "Soy el chatbot de Gemini, la aplicación de red social. " +
                                    "Los módulos disponibles son: 1) Perfil de usuario, 2) Mensajes, 3) Noticias, " +
                                    "y 4) Configuración. Puedes preguntarme sobre cualquiera de estos módulos y funcionalidades.";

                // Crear el prompt completo agregando el contexto.
                string fullPrompt = $"{appContext} Ahora, por favor, responde a la siguiente solicitud: {prompt}";

                // Crear el modelo de generador con la clave API y modelo deseado.
                GenerativeModel model = new GenerativeModel(_geminiSettings.API_KEY, "gemini-2.0-flash");

                // Llamada a la API para obtener la respuesta.
                var result = await model.GenerateContentAsync(fullPrompt);

                // Retornar la respuesta formateada.
                return new ChatbotResponse
                {
                    Reponse = $"Hola, soy el chatbot de Gemini. {result.Text()}"
                };
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error en la solicitud: " + e.Message);
            }
        }

    }


    }

