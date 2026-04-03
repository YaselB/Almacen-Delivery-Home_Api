using AlmacenApi.Aplication.Command.ComboOut.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Presentation.Controller.ComboOutController;
[ApiController]
[Route("api/comboOut")]
public class ComboOutController : ControllerBase
{
    private readonly IMediator mediator;
    public ComboOutController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost()]
    public async Task<IActionResult> Create(CreateComboOutEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
}