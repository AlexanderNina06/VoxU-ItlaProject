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
                string appContext = " Eres un asistente virtual para una plataforma que sirve como red social para estudiantes universitarios del Instituto tecnologico de las Americas ITLA." +
                    " \r\n\r\nLas funciones habiles que tiene la plataforma son: \r\n\r\n" +
                    "1. La ventana principal en la que puedes ver y hacer publicaciones, como preguntas, pedir recomendaciones, compartir opiniones e ideas, imagenes y cualquier publicacion que cumpla con las reglas de la plataforma.  \r\n\r\n" +
                    "2.La ventana de tienda en la que los usuarios podran vender y comprar diferentes articulos (dentro de las reglas de la plataforma)\r\n\r\n" +
                    "3. La ventana de biblioteca, donde los usuarios podran acceder a recursos de plataformas como Oreally (con su matricula del ITLA) y OpenLibrary (con cualquier correo electronico), ademas de poder subir sus propios recursos, como anotaciones, ensayos, libros, plantillas, etc. \r\n\r\n" +
                    "4. La ventana de perfil de usuario, donde estos podran actualizar su foto de perfil, su username, actualizar su contraseña y otras configuraciones.\r\n\r\n" +
                    "<Reglas>\r\n" +
                    "user: Cuales son las reglas?\r\n" +
                    "content: Las reglas de la plataforma incluyen:\r\n\r\nPUBLICACIONES\r\nNo subir ningun tipo de contenido pornografico\r\n\r\n" +
                    "No subir contenidos que inciten al odio como temas religiosos y politicos\r\n\r\n" +
                    "TIENDA\r\n" +
                    "No vender cosas ilicitas o ilegales en la tienda\r\n\r\n" +
                    "no vender cosas erotica's\r\n\r\n" +
                    "no vender bebidas alcoholicas\r\n\r\n" +
                    "BIBLIOTECA \r\n" +
                    "no subir libros para adultos como hentai y comics eroticos\r\n\r\n" +
                    "El incumplimiento de algunas de estas reglas puede ocasionar que sea baneado de la plataforma temporal o permanentemente dependiendo a gravedad de la infraccion.\r\n";

                // Crear el prompt completo agregando el contexto.
                string fullPrompt = $"{appContext} Ahora, por favor, responde a la siguiente solicitud: {prompt}";

                // Crear el modelo de generador con la clave API y modelo deseado.
                GenerativeModel model = new GenerativeModel(_geminiSettings.API_KEY, "gemini-2.0-flash");

                // Llamada a la API para obtener la respuesta.
                var result = await model.GenerateContentAsync(fullPrompt);

                // Retornar la respuesta formateada.
                return new ChatbotResponse
                {
                    Reponse = $"{result.Text()}"
                };
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Error en la solicitud: " + e.Message);
            }
        }

    }


    }

