using Confluent.Kafka;
using KafkaExample.Common;
using System.Text.Json;

var config = new ProducerConfig
{
    BootstrapServers = "localhost:9092"
};

using var producer = new ProducerBuilder<Null, string>(config).Build();

try
{
    for (int i = 0; i < 3; i++)
    {
        var result = await producer.ProduceAsync(
            "test-topic",
            new Message<Null, string> { 
                Value = JsonSerializer.Serialize(
                    new MyKafkaMessage 
                    { 
                        MessageValue = $"Message from producer {i}", 
                        Id = Guid.NewGuid() 
                    })
            });

        Console.WriteLine($"Delivered to: {result.TopicPartitionOffset}");
    }
}
catch (ProduceException<Null, string> e)
{
    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
}
