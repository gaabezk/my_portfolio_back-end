using Amazon.SQS.Model;
using MediatR;

namespace Application.Services;

public interface ISqsService
{
    public Task<Unit> SendMessageAsync(string queueUrl, string message, CancellationToken cancellationToken);
    Task<Message?> ReceiveMessageAsync(string queueUrl, CancellationToken cancellationToken);
    Task DeleteMessageAsync(string queueUrl, string receiptHandle, CancellationToken cancellationToken);

}