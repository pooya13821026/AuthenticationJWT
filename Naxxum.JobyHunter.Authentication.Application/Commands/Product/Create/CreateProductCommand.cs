using Authentication.Application.Common.Interfaces;
using MediatR;

namespace Authentication.Application.Commands.Product.Create;

public class CreateProductCommand : IRequest<bool>
{
    public string Name { get; set; }

    public DateTime ProductDate { get; set; }
    public string UserId { get; set; }
}

public class CreateProductCommandHandler(IProductService productService) : IRequestHandler<CreateProductCommand, bool>
{
    public async Task<bool> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var result = await productService.CreateProductAsync(request.Name, request.UserId);
        return result;
    }
}