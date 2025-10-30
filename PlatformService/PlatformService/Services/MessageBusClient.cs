using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using PlatformService.Models.DTOs;
using PlatformService.Services.Interfaces;
using RabbitMQ.Client;

namespace PlatformService.Services
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConfiguration _config;
        private readonly IConnection _connection;
        private readonly IChannel _channel;

        public MessageBusClient(IConfiguration config)
        {
            _config = config;
            try
            {
                var factory = new ConnectionFactory() { HostName = _config.GetSection("RabbitmqHOST").Value, Port = int.Parse(_config.GetSection("RabbitmqPORT").Value) };
                _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
                _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
                _channel.ExchangeDeclareAsync(exchange: "trigger", type: ExchangeType.Fanout, durable: true);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"---> ERROR OCCURED WHILE CONNECTING TO RABBITMQ: {ex.Message}");
            }
        }

        public async Task PublishNewPlatform(PlatformPublishDto dto)
        {
            string message = JsonSerializer.Serialize(dto);
            var body = Encoding.UTF8.GetBytes(message);
            await _channel.BasicPublishAsync(exchange: "trigger", routingKey: "", body: body);
            System.Console.WriteLine("---> Message sent to rabbitmq");
        }

        private async Task Dispose()
        {
            System.Console.WriteLine("---> Message Bus Disposed");
            if (_channel.IsOpen)
            {
                await _connection.CloseAsync();
                await _channel.CloseAsync();
            }
        }
        

    }

}