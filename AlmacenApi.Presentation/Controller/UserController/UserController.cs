using AlmacenApi.Aplication.Command.User.Create;
using AlmacenApi.Aplication.Command.User.Delete;
using AlmacenApi.Aplication.Command.User.Update;
using AlmacenApi.Aplication.Features.User.Dto;
using AlmacenApi.Aplication.Queries.User.GetAll;
using AlmacenApi.Aplication.Queries.User.GetById;
using AlmacenApi.Aplication.Queries.User.Login;
using AlmacenApi.Domain.Entities.User;
using AlmacenApi.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AlmacenApi.Aplication.Common.Security;
using AlmacenApi.Domain.Common.Permission;

namespace AlmacenApi.Presentation.Controller.User;
[ApiController]
[Route("user")]
public class UserController : GenericController<UserEntity, CreateUserEntityCommand, UpdateUserEntityCommand, UserResultDto>
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost()]
    [RequiredPermissionAtribute(Permissions.CreateUserPermission)]
    public override async Task<IActionResult> Create([FromBody] CreateUserEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpPut()]
    [RequiredPermissionAtribute(Permissions.UpdateUserPermission)]
    public override async Task<IActionResult> Update([FromBody] UpdateUserEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , result.error.Error); 
        }
        return Ok(result.Value);
    }
    [HttpDelete("{id}")]
    [RequiredPermissionAtribute(Permissions.DeleteUserPermission)]
    public async Task<IActionResult> Delete(string id)
    {
        var DeleteEntityCommand = new DeleteUserEntityCommand{Id = id};
        var result = await mediator.Send(DeleteEntityCommand);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new {error = result.error.Error});
        }    
        return Ok(result.Value);
    }
    [HttpGet("{id}")]
    [RequiredPermissionAtribute(Permissions.ReadOnlyUserPermission)]
    public async override Task<IActionResult> GetById(string id)
    {
        var query = new GetUserEntityByIdQuery
        {
            Id = id
        };
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpGet()]
    public override async Task<ActionResult<IEnumerable<UserEntity>>> GetAll()
    {
        var query = new GetAllUserEntitiesQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserEntityQuery query)
    {
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
}