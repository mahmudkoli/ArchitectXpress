namespace ArchitectXpress.RabbitMQ;

public interface IMessagePublisher
{
    Task PublishMessageAsync<T>(T data) where T : class;
}