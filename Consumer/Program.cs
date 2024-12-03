using MassTransit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Order.Interfaces;

namespace Consumer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.ReceiveEndpoint("order-created-event", e =>
                {
                    e.Consumer<OrderCreatedConsumer>();
                });
            });
            await busControl.StartAsync(new CancellationToken());
            try
            {
                Console.WriteLine("Press enter to exit");

                await Task.Run(() => Console.ReadLine());
            }
            finally
            {
                await busControl.StopAsync();
            }

        }
    }
}
