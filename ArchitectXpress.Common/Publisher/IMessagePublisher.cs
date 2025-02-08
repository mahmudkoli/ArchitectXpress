namespace ArchitectXpress.Common.Publisher;

public interface IMessagePublisher
{
    Task PublishMessageAsync<T>(T data) where T : class;
}