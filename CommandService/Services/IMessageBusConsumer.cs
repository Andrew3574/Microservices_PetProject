namespace CommandService.Services
{
    public interface IMessageBusConsumer : IHostedService
    {
        void ListenEvents();
    }
}