namespace MusicLibraryWebAPI.RabbitMQ
{
    public interface IMessageProducer
    {
        Task SendMessage<T>(T message);
    }
}
