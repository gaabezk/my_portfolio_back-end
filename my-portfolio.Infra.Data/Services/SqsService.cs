using Amazon.SQS;
using Amazon.SQS.Model;
using Application.Services;
using MediatR;

namespace Infra.Services
{
    public class SqsService(IAmazonSQS sqsClient) : ISqsService
    {
        public async Task<Unit> SendMessageAsync(string queueUrl, string message, CancellationToken cancellationToken)
        {
            try
            {
                var sendRequest = new SendMessageRequest
                {
                    QueueUrl = queueUrl,
                    MessageBody = message
                };

                await sqsClient.SendMessageAsync(sendRequest, cancellationToken);
                return Unit.Task.Result;
            }
            catch (AmazonSQSException sqsEx)
            {
                throw new AmazonSQSException($"AWS SQS error: {sqsEx.Message}");
            }
            catch (Exception ex)
            {
                throw new Exception($"General error: {ex.Message}");
            }
        }
    }
}