using System;
using Confluent.Kafka;


var config = new ConsumerConfig
{
    BootstrapServers = "localhost:9092",
    GroupId = "test-group",
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
consumer.Subscribe("test-topic");

Console.WriteLine("Listening for messages...");

try
{
    while (true)
    {
        var cr = consumer.Consume();
        Console.WriteLine($"Received: {cr.Message.Value}");
    }
}
catch (OperationCanceledException)
{
    consumer.Close();
}
