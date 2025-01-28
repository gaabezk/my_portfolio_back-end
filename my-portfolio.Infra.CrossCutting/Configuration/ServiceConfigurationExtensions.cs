
using Amazon.S3;
using Microsoft.AspNetCore.Builder;
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
    }
    
    public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
    {
        // Configuração do banco de dados (PostgreSQL)
        // Você pode configurar o banco de dados aqui como quiser
        // builder.Services.AddDbContext<AppDbContext>(options =>
        //     options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
    }
}