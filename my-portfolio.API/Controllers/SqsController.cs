using Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SqsController : BaseController
{
    [HttpPost("post-message")]
    public async Task<IActionResult> PostMessage([FromQuery] SendMessageToQueueCommand command, [FromServices] IMediator mediator)
    {
        try
        {
            var success = await mediator.Send(command);
            
            return SuccessResponse<string>(null,"Mensagem enviada com sucesso.");
        }
        catch (ArgumentException ex)
        {
            return ErrorResponse(ex.Message, 400);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}