using Authentication.Application.Commands.Product.Create;
using Authentication.Application.Commands.Product.Delete;
using Authentication.Application.Commands.Product.Update;
using Authentication.Application.DTOs;
using Authentication.Application.Queries.Product;
using Authentication.Infra.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IMediator mediator, UserManager<ApplicationUser> userManager) : ControllerBase
    {
        [HttpGet("GetAll")]
        [ProducesDefaultResponseType(typeof(List<ProductDTO>))]
        public async Task<IActionResult> GetAllProductAsync()
        {
            return Ok(await mediator.Send(new GetProductQuery()));
        }

        [HttpPost("Create")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> CreateProduct(string name)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(User);
                return Ok(await mediator.Send(new CreateProductCommand()
                {
                    Name = name,
                    UserId = userId,
                }));
            }

            return BadRequest();
        }

        [HttpDelete("Delete/{productId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(User);

                var result = await mediator.Send(new DeleteProductCommand() { ProductId = productId, UserId = userId });
                return Ok(result);
            }

            return BadRequest();
        }

        [HttpPut("UpdateProduct/{productId}")]
        [ProducesDefaultResponseType(typeof(int))]
        public async Task<ActionResult> UpdateProduct(int productId, string name, bool isAvailable)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = userManager.GetUserId(User);

                var result = await mediator.Send(new UpdateProductCommand()
                {
                    ProductId = productId,
                    UserId = userId,
                    Name = name,
                    IsAvailable = isAvailable,
                });
                return Ok(result);
            }

            return BadRequest();
        }
    }
}