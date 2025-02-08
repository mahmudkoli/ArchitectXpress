namespace ArchitectXpressWorker.RabbitMQ;

public class RabbitMQSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
    public string FareQueueName { get; set; }
    public string FareExchangeName { get; set; }
}
