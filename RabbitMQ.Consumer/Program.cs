using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace RabbMQ.Consumer;

internal class Program
{
    static void Main(string[] args)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "SAUDACAO_1", durable: false, exclusive: false, autoDelete: true, arguments: null);


                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };
                
                channel.BasicConsume(queue: "SAUDACAO_1", autoAck: true, consumer: consumer);
            }
        }
    }
}