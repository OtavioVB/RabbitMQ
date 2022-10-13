using RabbitMQ.Client;
using System.Text;

namespace RabbMQ.Publisher;

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
                channel.BasicPublish(exchange: "", routingKey: "SAUDACAO_1", basicProperties: null, body: Encoding.UTF8.GetBytes("Primeira vez no RabbitMQ"));
            }
        }
    }
}