using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using MusicLibraryWebAPI.Models;
using Newtonsoft.Json;
using MusicLibraryWebAPI.Data;

namespace MusicLibraryWebAPI.Services
{
    public class RabbitMQConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public RabbitMQConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            while (!stoppingToken.IsCancellationRequested)
            {
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("songs", false, false, true, null);

                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += async (model, eventArgs) =>
                    {
                        var body = eventArgs.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        try
                        {
                            using (var scope = _serviceProvider.CreateScope())
                            {
                                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                                var song = JsonConvert.DeserializeObject<Song>(message);
                                await dbContext.Songs.AddAsync(song);
                                await dbContext.SaveChangesAsync();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }
                    };

                    channel.BasicConsume(queue: "songs", autoAck: true, consumer: consumer);
                }

                if (stoppingToken.IsCancellationRequested)
                {
                    break;
                }

                // Wait for the cancellation token to be signaled
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
        }
    }
}
