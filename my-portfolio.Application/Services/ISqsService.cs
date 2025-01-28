using MediatR;

namespace Application.Services;

public interface ISqsService
{
    public Task<Unit> SendMessageAsync(string queueUrl, string message, CancellationToken cancellationToken);
}