
using Amazon.S3;
using Amazon.SQS;
using Infra.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace my_portfolio.Infra.CrossCutting.Configuration;

public static class ServiceConfigurationExtensions
{
    public static void AddAwsServices(this WebApplicationBuilder builder)
    {
        var awsOptions = builder.Configuration.GetAWSOptions();
        builder.Services.AddDefaultAWSOptions(awsOptions);
        builder.Services.AddAWSService<IAmazonS3>();
        builder.Services.AddAWSService<IAmazonSQS>();
    }
    
    public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        // Adiciona o DbContext com a string de conexão
        builder.Services.AddDbContext<DefaultDbContext>(options =>
            options.UseMySql(
                builder.Configuration.GetConnectionString("DefaultConnection"),
                new MySqlServerVersion(new Version(8, 0, 39)) // Versão do MySQL
            ));
    }
}