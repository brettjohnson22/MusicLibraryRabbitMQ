using MusicLibraryWebAPI.RabbitMQ.Connection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json.Serialization;

namespace MusicLibraryWebAPI.RabbitMQ
{
    public class RabbitMqProducer : IMessageProducer
    {
        private readonly IRabbitMqConnection _connection;

        public RabbitMqProducer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public async Task SendMessage<T>(T message)
        {
            try
            {
                using var channel = _connection.Connection.CreateModel();
                channel.QueueDeclare("songs", exclusive: false);

                //var messageObject = new { Operation = operation, Message = message };
                var json = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(json);

                await Task.Run(() =>
                {
                    channel.BasicPublish(exchange: "", routingKey: "songs", body: body);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending message: {ex.Message}");
            }
        }
    }
}
