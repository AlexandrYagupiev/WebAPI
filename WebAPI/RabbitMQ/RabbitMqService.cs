using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using WebAPI.RabbitMQ;

public class RabbitMqService : IRabbitMqService
{
    public async void SendMessage(object obj)
    {
        var message = JsonSerializer.Serialize(obj);
        await SendMessage(message);
    }

    public async Task SendMessage(string message)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(queue: "MyQueue",
                       durable: false,
                       exclusive: false,
                       autoDelete: false,
                       arguments: null);

        var body = Encoding.UTF8.GetBytes(message);

        await channel.BasicPublishAsync(exchange: string.Empty,
            routingKey: "MyQueue",
            body: body);

    }

   
}