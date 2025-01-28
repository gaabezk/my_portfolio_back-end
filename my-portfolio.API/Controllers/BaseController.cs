using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController: ControllerBase
{
    /// <summary>
    /// Retorna uma resposta padrão para operações bem-sucedidas.
    /// </summary>
    /// <typeparam name="T">Tipo do dado retornado.</typeparam>
    /// <param name="data">Dados a serem retornados.</param>
    /// <param name="message">Mensagem opcional.</param>
    /// <returns>Resposta padronizada.</returns>
    protected IActionResult SuccessResponse<T>(T? data, string message = "Operation successful.")
    {
        return Ok(new
        {
            Success = true,
            Message = message,
            Data = data
        });
    }

    /// <summary>
    /// Retorna uma resposta padrão para erros.
    /// </summary>
    /// <param name="message">Mensagem de erro.</param>
    /// <param name="statusCode">Código de status HTTP.</param>
    /// <returns>Resposta de erro padronizada.</returns>
    protected IActionResult ErrorResponse(string message, int statusCode = 400)
    {
        return StatusCode(statusCode, new
        {
            Success = false,
            Message = message
        });
    }

    /// <summary>
    /// Trata exceções gerais e retorna um erro padronizado.
    /// </summary>
    /// <param name="ex">Exceção capturada.</param>
    /// <returns>Resposta de erro padronizada.</returns>
    protected IActionResult HandleException(Exception ex)
    {
        // Log da exceção pode ser adicionado aqui
        return ErrorResponse($"An unexpected error occurred: ${ex}", 500);
    }
}