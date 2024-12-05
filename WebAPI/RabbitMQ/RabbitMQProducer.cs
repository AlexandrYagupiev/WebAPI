using static WebAPI.RabbitMQ.Producer;
using RabbitMQ.Client;

namespace WebAPI.RabbitMQ
{
    public class RabbitMQProducer : IMessageProducer
    {
        public void SendMessage<T>(T message)
        {
            var _factory = new ConnectionFactory { HostName = "localhost" };
            var con = _factory.CreateConnectionAsync();
           // using con 
        }
    }
}
