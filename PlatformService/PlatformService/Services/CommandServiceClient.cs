using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using PlatformService.Models.DTOs;
using PlatformService.Services.Interfaces;

namespace PlatformService.Services
{
    public class CommandServiceClient : ICommandServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public CommandServiceClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task SendPlatformHttp(PlatformReadDto dto)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"
            );
            var response = await _httpClient.PostAsync($"{_config.GetConnectionString("CommandService")}/api/c/command", content);
            if (response.IsSuccessStatusCode) System.Console.WriteLine("successfully sent");
            
        }
    }
}