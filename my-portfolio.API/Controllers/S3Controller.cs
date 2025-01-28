using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class S3Controller : BaseController
{
    [HttpPost("upload-file")]
    public async Task<IActionResult> UploadFile(IFormFile? file, [FromQuery] string? bucketName, [FromServices] IMediator mediator)
    {
        try
        {
            // Validação do arquivo
            if (file == null || file.Length == 0)
            {
                return ErrorResponse("O arquivo enviado é inválido.");
            }

            if (string.IsNullOrEmpty(bucketName))
            {
                return ErrorResponse("O nome do bucket não pode estar vazio.");
            }

            // Criação do command com os dados do arquivo
            var command = new UploadFileToS3Command
            {
                File = file,
                BucketName = bucketName
            };

            // Enviar o command para o Mediator
            await mediator.Send(command);

            // Retornar uma resposta apropriada
            return SuccessResponse<string>(null,"Arquivo enviado com sucesso.");
        }
        catch (ArgumentException ex)
        {
            // Trata erros específicos, como validações do lado do Mediator ou command
            return ErrorResponse(ex.Message, 400);
        }
        catch (Exception ex)
        {
            // Trata erros inesperados e retorna um erro padronizado
            return HandleException(ex);
        }
    }
}