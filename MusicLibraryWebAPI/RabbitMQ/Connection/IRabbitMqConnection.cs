using RabbitMQ.Client;

namespace MusicLibraryWebAPI.RabbitMQ.Connection
{
    public interface IRabbitMqConnection
    {
        IConnection Connection { get; }
    }
}
