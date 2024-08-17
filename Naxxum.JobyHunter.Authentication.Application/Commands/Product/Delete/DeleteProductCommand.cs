using Authentication.Application.Common.Interfaces;
using MediatR;

namespace Authentication.Application.Commands.Product.Delete;

public class DeleteProductCommand : IRequest<bool>
{
    public int ProductId { get; set; }
    public string UserId { get; set; }
}

public class DeleteProductCommandHandler(IProductService ProductService) : IRequestHandler<DeleteProductCommand, bool>
{
    public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await ProductService.DeleteProductAsync(request.ProductId, request.UserId);

        return result;
    }
}