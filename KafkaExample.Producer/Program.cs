using System;
using System.Threading.Tasks;
using Confluent.Kafka;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    var result = await producer.ProduceAsync(
        "test-topic",
        new Message<Null, string> { Value = "Message from producer" });

    Console.WriteLine($"Delivered to: {result.TopicPartitionOffset}");
}
catch (ProduceException<Null, string> e)
{
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}
