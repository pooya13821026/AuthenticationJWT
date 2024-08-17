using System.ComponentModel.DataAnnotations;
using Authentication.Application.Common.Interfaces;
using MediatR;

namespace Authentication.Application.Commands.Product.Update;

public class UpdateProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }

    public string UserId { get; set; }

    public string Name { get; set; }
    public bool IsAvailable { get; set; }
}

public class UpdateProductCommandHandler(IProductService productService)
    : IRequestHandler<UpdateProductCommand, bool>
{
    public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var result =
            await productService.UpdateProduct(request.ProductId, request.UserId, request.Name, request.IsAvailable);
        return result;
    }
}