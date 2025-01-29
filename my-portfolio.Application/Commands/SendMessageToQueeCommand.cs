using Application.Interfaces.Services;
using MediatR;

namespace Application.Commands;

public class SendMessageToQueueCommand : IRequest<Unit>
{
    public string QueueUrl { get; set; }
    public string Message { get; set; }
}

public class SendMessageToQueueCommandHandler(ISqsService sqsService) : IRequestHandler<SendMessageToQueueCommand, Unit>
{
    public async Task<Unit> Handle(SendMessageToQueueCommand request, CancellationToken cancellationToken)
    {
        return await sqsService.SendMessageAsync(request.QueueUrl, request.Message, cancellationToken);;
    }
}
