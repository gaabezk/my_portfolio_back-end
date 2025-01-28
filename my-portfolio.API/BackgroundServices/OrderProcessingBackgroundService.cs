using Application.Services;
using Domain.Models.Entities;
using Newtonsoft.Json;

namespace API.BackgroundServices;

public class OrderProcessingBackgroundService(ISqsService sqsService, string queueUrl) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var message = await sqsService.ReceiveMessageAsync(queueUrl, stoppingToken);

            if (message != null)
            {
                // Valida se a mensagem é uma ordem (Order)
                if (IsValidOrderMessage(message.Body))
                {
                    await ProcessOrderAsync(message.Body);
                    await sqsService.DeleteMessageAsync(queueUrl, message.ReceiptHandle, stoppingToken);
                }
                else
                {
                    await HandleInvalidMessageAsync(message.Body);
                    await sqsService.DeleteMessageAsync(queueUrl, message.ReceiptHandle, stoppingToken);
                }
            }

            await Task.Delay(5000, stoppingToken);  // Delay para evitar sobrecarga
        }
    }

    private bool IsValidOrderMessage(string message)
        {
            try
            {
                var order = JsonConvert.DeserializeObject<Order>(message);
                return order != null && order.Products != null && order.Products.Any();
            }
            catch (JsonException)
            {
                return false;
            }
        }

        private async Task ProcessOrderAsync(string message)
        {
            var order = JsonConvert.DeserializeObject<Order>(message);

            if (order == null)
            {
                Console.WriteLine($"Falha ao processar o pedido: {message}");
                return;
            }

            Console.WriteLine($"Processando pedido {order.Id} com {order.Products.Count()} produtos");
            
            await SaveOrderToDatabaseAsync(order);
        }

        private async Task HandleInvalidMessageAsync(string message)
        {
            Console.WriteLine($"Mensagem inválida recebida: {message}");
            await SaveInvalidMessageToDatabaseAsync(message);
        }

        private async Task SaveOrderToDatabaseAsync(Order order)
        {
            Console.WriteLine($"Salvando pedido {order.Id} no banco de dados...");
            // await _orderRepository.SaveAsync(order);
        }

        private async Task SaveInvalidMessageToDatabaseAsync(string message)
        {
            Console.WriteLine($"Registrando mensagem inválida no banco de dados: {message}");
            // await _invalidMessageRepository.SaveAsync(message);
        }
}
