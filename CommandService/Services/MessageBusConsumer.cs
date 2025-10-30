
using System.Text;
using System.Text.Unicode;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace CommandService.Services
{
    public class MessageBusConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IChannel _channel;
        private readonly string _queueName;

        public MessageBusConsumer(IConfiguration configuration)
        {
            var factory = new ConnectionFactory()
            {
                HostName = configuration.GetSection("RabbitmqHOST").Value,
                Port = int.Parse(configuration.GetSection("RabbitmqPORT").Value)
            };
            _connection = factory.CreateConnectionAsync().GetAwaiter().GetResult();
            _channel = _connection.CreateChannelAsync().GetAwaiter().GetResult();
            _channel.ExchangeDeclareAsync(
                exchange: "trigger",
                type: ExchangeType.Fanout,
                durable: true).GetAwaiter().GetResult();
            _queueName = _channel.QueueDeclareAsync().GetAwaiter().GetResult().QueueName;
            _channel.QueueBindAsync(queue:_queueName,exchange:"trigger",routingKey:"");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new AsyncEventingBasicConsumer(_channel);
            consumer.ReceivedAsync +=  async (handler, ea) =>
            {
                var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                System.Console.WriteLine($"--> MESSAGE ACCEPTED {message}");
            };
            await _channel.BasicConsumeAsync(
                queue:_queueName,
                autoAck:true,
                consumer:consumer);
        }

        public override void Dispose()
        {
            _channel.Dispose();
            _connection.Dispose();
        }
    }
}