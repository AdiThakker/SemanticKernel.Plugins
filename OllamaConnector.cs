using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SemanticKernel.AI.ChatCompletion;

namespace SemanticKernel.Plugins
{
    public class OllamaConnector : IChatCompletion
    {
        private readonly HttpClient _httpClient;

        public OllamaConnector()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> CompleteAsync(string prompt, CancellationToken cancellationToken = default)
        {
            // Create the request payload
            var requestPayload = new { prompt };
            var content = new StringContent(JsonSerializer.Serialize(requestPayload), Encoding.UTF8, "application/json");

            // Send the request to the Ollama server
            var response = await _httpClient.PostAsync("http://localhost:11434/api/chat", content, cancellationToken);
            response.EnsureSuccessStatusCode();

            // Parse the response
            var responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonSerializer.Deserialize<JsonDocument>(responseBody);

            // Extract and return the generated text
            return jsonResponse?.RootElement.GetProperty("response").GetString() ?? string.Empty;
        }
    }
}

