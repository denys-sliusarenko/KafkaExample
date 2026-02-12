using Confluent.Kafka;
using KafkaExample.Common;
using System.Text.Json;


var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "test-group",
    AutoOffsetReset = AutoOffsetReset.Earliest,
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("test-topic");

Console.WriteLine("Listening for messages...");

try
{
    while (true)
    {
        var cr = consumer.Consume();

        var message = JsonSerializer.Deserialize<MyKafkaMessage>(cr.Message.Value);

        Console.WriteLine($"Received: {message.Id} : {message.MessageValue}");
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}
