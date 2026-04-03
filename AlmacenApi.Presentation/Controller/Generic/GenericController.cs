using AlmacenApi.Aplication.Command.Generic.Create;
using AlmacenApi.Aplication.Command.Generic.Delete;
using AlmacenApi.Aplication.Command.Generic.Update;
using AlmacenApi.Aplication.Queries.AdminQueries.GetAll;
using AlmacenApi.Aplication.Queries.Generic.GetAll;
using AlmacenApi.Aplication.Queries.Generic.GetById;
using AlmacenApi.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AlmacenApi.Presentation.Controller.Generic
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenericController<TEntity,TCreateCommand , TUpdateCommand , TDto> : ControllerBase 
        where TEntity : GenericEntity<TEntity>,
        new () where TCreateCommand : CreateGenericEntityCommand<TEntity>,
        new () where TUpdateCommand : UpdateGenericEntityCommand<TEntity>
        where TDto : class
    {
        protected readonly IMediator mediator;

        public GenericController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create([FromBody] TCreateCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return BadRequest(result.error);
            }
            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update( [FromBody] TUpdateCommand command)
        {
            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(result.error);
            }
            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteGenericEntityCommand<TEntity> { Id = id };
            var result = await mediator.Send(command);
            if (result.IsFailure)
            {
                return NotFound(result.error);
            }
            return Ok(result.Value);
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(string id)
        {
            var query = new GetGenericEntityByIdQuery<TEntity,TDto> { Id = id };
            var result = await mediator.Send(query);
            if (result.IsFailure)
            {
                return NotFound(result.error);
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public virtual async Task<ActionResult<IEnumerable<TEntity>>> GetAll()
        {
            var query = new GetAllGenericEntityQuery<TEntity , TDto>();
            var result = await mediator.Send(query);
            return Ok(result.Value);
        }
    }
}