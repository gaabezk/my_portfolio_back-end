using Amazon.SQS;
using Amazon.SQS.Model;

namespace Infra.Services;

public class SqsService(IAmazonSQS sqsClient)
{
    public async Task SendMessageAsync(string queueUrl, string message)
    {
        var sendRequest = new SendMessageRequest
        {
            QueueUrl = queueUrl,
            MessageBody = message
        };

        await sqsClient.SendMessageAsync(sendRequest);
    }
}
