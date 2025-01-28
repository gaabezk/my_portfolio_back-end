using Amazon.SQS;
using Amazon.SQS.Model;
using Application.Services;
using MediatR;

namespace Infra.Services;

public class SqsService(IAmazonSQS sqsClient) : ISqsService
{
    public async Task<Unit> SendMessageAsync(string queueUrl, string message, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(queueUrl))
            throw new ArgumentException("Queue URL cannot be null or empty.", nameof(queueUrl));

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentException("Message body cannot be null or empty.", nameof(message));

        try
        {
            var sendRequest = new SendMessageRequest
            {
                QueueUrl = queueUrl,
                MessageBody = message
            };

            var response = await sqsClient.SendMessageAsync(sendRequest, cancellationToken);
            
            Console.WriteLine($"Message sent successfully. MessageId: {response.MessageId}");

            return Unit.Value;
        }
        catch (AmazonSQSException sqsEx)
        {
            Console.WriteLine($"AWS SQS error: {sqsEx.Message}");
            throw new AmazonSQSException($"AWS SQS error: {sqsEx.Message}", sqsEx);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"General error occurred while sending message: {ex.Message}");
            throw new Exception($"An error occurred while sending the message: {ex.Message}", ex);
        }
    }
    
    public async Task<Message?> ReceiveMessageAsync(string queueUrl, CancellationToken cancellationToken)
    {
        var receiveRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrl,
            MaxNumberOfMessages = 1, // Lê apenas uma mensagem por vez
            WaitTimeSeconds = 10,    // Long polling de 10 segundos
            VisibilityTimeout = 30   // Tempo de invisibilidade para processamento
        };

        var response = await sqsClient.ReceiveMessageAsync(receiveRequest, cancellationToken);

        // Retorna a primeira mensagem ou null
        if (response.Messages.Count > 0)
        {
            Console.WriteLine($"Mensagem recebida: {response.Messages[0].Body}");
            return response.Messages[0];
        }

        Console.WriteLine("Nenhuma mensagem encontrada.");
        return null;
    }

    public async Task DeleteMessageAsync(string queueUrl, string receiptHandle, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(receiptHandle))
        {
            Console.WriteLine("O ReceiptHandle fornecido é inválido.");
            return;
        }

        try
        {
            var deleteRequest = new DeleteMessageRequest
            {
                QueueUrl = queueUrl,
                ReceiptHandle = receiptHandle
            };

            var response = await sqsClient.DeleteMessageAsync(deleteRequest, cancellationToken);
            Console.WriteLine($"Mensagem com ReceiptHandle '{receiptHandle}' deletada com sucesso.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao deletar a mensagem: {ex.Message}");
            throw;
        }
    }
}
