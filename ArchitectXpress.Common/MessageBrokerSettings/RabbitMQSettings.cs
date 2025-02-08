namespace ArchitectXpress.Common.MessageBrokerSettings;

public class RabbitMQSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string FareCalculationQueueName { get; set; }
    public string FareCalculationExchangeName { get; set; }
    public string FareCalculatedQueueName { get; set; }
    public string FareCalculatedExchangeName { get; set; }
}
