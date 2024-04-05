
using Microsoft.EntityFrameworkCore;
using MusicLibraryWebAPI.Data;
using MusicLibraryWebAPI.RabbitMQ;
using MusicLibraryWebAPI.RabbitMQ.Connection;
using MusicLibraryWebAPI.Services;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace MusicLibraryWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 31));
            builder.Services.AddDbContext<ApplicationDbContext>(dbContextOptions => dbContextOptions
            .UseMySql(connectionString, serverVersion)
            .EnableDetailedErrors());

            // Add RabbitMQ Consumer
            builder.Services.AddHostedService<RabbitMQConsumer>();

            builder.Services.AddSingleton<IRabbitMqConnection>(new RabbitMqConnection());
            builder.Services.AddScoped<IMessageProducer, RabbitMqProducer>();
            builder.Services.AddScoped<ISongService, SongService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}