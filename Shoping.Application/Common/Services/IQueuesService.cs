namespace Shoping.Application.Common.Services;
public interface IQueuesService
{
    Task QueueAsync<T>(string queueName, T item);
}
