using System.Text;
using RabbitMQ.Client;

//Basic Producer
var factory = new ConnectionFactory { HostName = "localhost" };

using var connection = factory.CreateConnection();

using var channel = connection.CreateModel();

channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);

var message = "I am broadcasting this message for different consumers";

var encodedMessage = Encoding.UTF8.GetBytes(message);

channel.BasicPublish(exchange: "pubsub", "", null, encodedMessage);

Console.WriteLine($"Published message: {message}");
