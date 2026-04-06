using AlmacenApi.Aplication.Command.Product.Create;
using AlmacenApi.Aplication.Command.Product.Delete;
using AlmacenApi.Aplication.Command.Product.Update;
using AlmacenApi.Aplication.Features.Product.Dto;
using AlmacenApi.Aplication.Queries.Product;
using AlmacenApi.Aplication.Queries.Product.GetById;
using AlmacenApi.Aplication.Common.Security;
using AlmacenApi.Domain.Entities.Product;
using AlmacenApi.Presentation.Controller.Generic;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AlmacenApi.Domain.Common.Permission;
using AlmacenApi.Aplication.Features.Combo.Dto;

namespace AlmacenApi.Presentation.Controller;

[ApiController]
[Route("api/product")]
public class ProductController : GenericController<ProductEntity, CreateProductEntityCommand, UpdateProductEntityCommand, ProductResultDto>
{
    public ProductController(IMediator mediator) : base(mediator)
    {
    }
    [HttpPost()]
    
    public override async Task<IActionResult> Create(CreateProductEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpPut()]
    
    public override async Task<IActionResult> Update([FromBody] UpdateProductEntityCommand command)
    {
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpDelete("{id}")]
    
    public async Task<IActionResult> Delete(string id , [FromBody] DeleteProductDto deleteProductDto)
    {
        var command = new DeleteProductEntityCommand
        {
            Id = id,
            AdminId = deleteProductDto.AdminId,
            UserId = deleteProductDto.UserId
        };
        var result = await mediator.Send(command);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpGet()]
    
    public override async Task<ActionResult<IEnumerable<ProductEntity>>> GetAll()
    {
        var query = new GetAllProductEntityQuery();
        var result = await mediator.Send(query);
        if(result.IsFailure && result.error != null)
        {
            return StatusCode(result.error.StatusCode , new { error = result.error.Error});
        }
        return Ok(result.Value);
    }
    [HttpGet("{id}")]
    
    public override async Task<IActionResult> GetById(string id)
    {
        var query = new GetProductEntityByIdQuery
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
}