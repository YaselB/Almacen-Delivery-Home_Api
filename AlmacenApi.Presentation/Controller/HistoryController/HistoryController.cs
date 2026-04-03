using AlmacenApi.Aplication.Queries.History.GetAll;
using AlmacenApi.Domain.Entities.History;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Presentation.Controller.HistoryController;
[ApiController]
[Route("api/history")]
public class HistoryController : ControllerBase
{
    private readonly IMediator mediator;
    public HistoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }
    [HttpGet()]
    public async Task<ActionResult<IEnumerable<HistoryEntity>>> GetAll()
    {
        var query = new GetAllHistoryEntityQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new {error = result.error.Error});
        }
        return Ok(result.Value);
    }
}