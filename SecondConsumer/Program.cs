using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
//Basic example
var factory = new ConnectionFactory { HostName = "localhost" };

var connection = factory.CreateConnection();

var channel = connection.CreateModel();
Console.WriteLine("Wating for the Producer");
channel.ExchangeDeclare(exchange: "pubsub", type: ExchangeType.Fanout);
var queueName = channel.QueueDeclare().QueueName;

var consumer = new EventingBasicConsumer(channel);
channel.QueueBind(queue: queueName, exchange: "pubsub", routingKey: "");
consumer.Received += (model, ea) =>
{
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    Console.WriteLine($"Second Consumer: Received message {message}");
};
channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
Console.ReadKey();
