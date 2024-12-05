namespace WebAPI.RabbitMQ
{
    public class Producer
    {
        public interface IMessageProducer
        {
            void SendMessage<T>(T message);
        }
    }
}
