using System.Reflection;
using API.BackgroundServices;
using API.Configuration;
using Application.Commands;
using Application.Services;
using Infra.Services;
using MediatR;
using Microsoft.OpenApi.Models;
using my_portfolio.Infra.CrossCutting.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IS3Service, S3Service>();
builder.Services.AddSingleton<ISqsService, SqsService>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "File Upload API", Version = "v1" });

    // Configuração para suportar envio de arquivos
    c.OperationFilter<SwaggerFileUploadOperationFilter>();
});
// Adicionar serviços de controllers
builder.Services.AddControllers();
builder.AddAwsServices();
builder.AddDatabaseConfiguration();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetAssembly(typeof(UploadFileToS3Command))!));

// Configurar o BackgroundService para ouvir a fila SQS
builder.Services.AddHostedService<OrderProcessingBackgroundService>(serviceProvider =>
{
    var sqsService = serviceProvider.GetRequiredService<ISqsService>();
    var queueUrl = builder.Configuration["SqsQueueUrl"]; // URL da fila SQS, armazenada na configuração
    return new OrderProcessingBackgroundService(sqsService, queueUrl!);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();