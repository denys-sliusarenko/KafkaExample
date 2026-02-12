namespace KafkaExample.Common;

public record MyKafkaMessage
{
    public Guid Id { get; set;  }
    public string MessageValue { get; set; }
}
