using System.Text;
using System.Text.Json;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.http
{
    public class HttpCommandDataClient(HttpClient client, IConfiguration configuration) : ICommandDataClient
    {
        private readonly HttpClient _client = client;
        private readonly IConfiguration _configuration = configuration;

        public async Task SendPlatformToCommand(PlatformReadDto platform)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(platform),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _client.PostAsync(_configuration["CommandServiceUri"], httpContent);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("SendPlatformToCommand is OK");
                System.Console.WriteLine($"response: {response.ToString()}");
            }
            else
            {
                Console.WriteLine("SendPlatformToCommand is not OK");
                System.Console.WriteLine($"response: {response.ToString()}");
            }
        }
    }
}