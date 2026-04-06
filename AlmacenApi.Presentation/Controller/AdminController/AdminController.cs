using AlmacenApi.Aplication.Command.AdminCommand.Create;
using AlmacenApi.Aplication.Command.AdminCommand.Delete;
using AlmacenApi.Aplication.Command.AdminCommand.Update;
using AlmacenApi.Aplication.Common.Security;
using AlmacenApi.Aplication.Features.Dto.ResultDto;
using AlmacenApi.Aplication.Queries.AdminQueries.GetAll;
using AlmacenApi.Aplication.Queries.AdminQueries.GetById;
using AlmacenApi.Aplication.Queries.AdminQueries.Login;
using AlmacenApi.Domain.Common.Permission;
using AlmacenApi.Domain.Entities.Admin;
using AlmacenApi.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Presentation.Controller.AdminController
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : GenericController<AdminEntity, CreateAdminEntityCommand, UpdateAdminEntityCommand, ResultAdminDto>
    {
        public AdminController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public override async Task<IActionResult> Create([FromBody] CreateAdminEntityCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsFailure && result.error != null)
            {
                return StatusCode(result.error.StatusCode, new { error = result.error.Error });
            }
            return Ok(result.Value);
        }
        [HttpGet("{id}")]
        public override async Task<IActionResult> GetById(string id)
        {
            var query = new GetAdminEntityById { Id = id };
            var result = await mediator.Send(query);
            if (result.IsFailure && result.error != null)
                return StatusCode(result.error.StatusCode, new { error = result.error.Error });
            return Ok(result.Value);
        }

        [HttpGet]
        
        public override async Task<ActionResult<IEnumerable<AdminEntity>>> GetAll()
        {
            var query = new GetAllAdminQuery();
            var result = await mediator.Send(query);
            if (result.IsFailure && result.error != null)
            {
                return StatusCode(result.error.StatusCode , new {error = result.error.Error});
            }
            return Ok(result.Value);

        }

        [HttpPut()]
        public override async Task<IActionResult> Update([FromBody] UpdateAdminEntityCommand command)
        {

            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(new
                {
                    error = result.error,
                    statusCode = 404
                });
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteAdminEntityCommand { Id = id };
            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(result.error);
            }
            return Ok(result.Value);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAdminEntityQuery login)
        {
            var result = await mediator.Send(login);
            if (result.IsFailure && result.error != null)
            {
               return StatusCode(result.error.StatusCode ,new {error = result.error.Error}); 
            }
            return Ok(result.Value);      
        }
    }
}