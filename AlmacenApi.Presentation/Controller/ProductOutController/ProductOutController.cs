using AlmacenApi.Aplication.Command.ProductOut.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Presentation.Controller.ProductOut;
[ApiController]
[Route("api/productOut")]
public class ProductOutController : ControllerBase
{
    private readonly IMediator mediator;
    public ProductOutController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpPost()]
    public async Task<IActionResult> Create(CreateProductOutEntityCommand request)
    {
        var result = await mediator.Send(request);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
}