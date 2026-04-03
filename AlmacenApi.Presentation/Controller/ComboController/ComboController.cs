using AlmacenApi.Aplication.Command.Combo.Create;
using AlmacenApi.Aplication.Command.Combo.Delete;
using AlmacenApi.Aplication.Command.Combo.Update;
using AlmacenApi.Aplication.Features.Combo.Dto;
using AlmacenApi.Aplication.Queries.Combo.GetAll;
using AlmacenApi.Aplication.Queries.Combo.GetById;
using AlmacenApi.Domain.Entities.Combo;
using AlmacenApi.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Presentation.ComboController;

[ApiController]
[Route("api/combo")]
public class ComboController : GenericController<ComboEntity, CreateComboEntityCommand, UpdateComboEntityCommand, ComboResultDto>
{
    public ComboController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost()]
    public override async Task<IActionResult> Create([FromBody] CreateComboEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode ,new {error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpPut()]
    public override async Task<IActionResult> Update([FromBody] UpdateComboEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpDelete("{id}")]
    public override async Task<IActionResult> Delete(string id)
    {
        var command = new DeleteComboEntityCommand
        {
            Id = id
        };
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new {error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(string id)
    {
        var query = new GetComboEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new {error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpGet()]
    public override async Task<ActionResult<IEnumerable<ComboEntity>>> GetAll()
    {
        var query = new GetAllComboEntityQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
           return StatusCode(result.error.StatusCode , new {error = result.error.Error}); 
        }
        return Ok(result.Value);
    }
}
