using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using MediatR;

namespace Authentication.Application.Queries.Product;

public class GetProductQuery : IRequest<List<ProductDTO>>
{
}

public class GetProductQueryHandler(IProductService productService)
    : IRequestHandler<GetProductQuery, List<ProductDTO>>
{
    public async Task<List<ProductDTO>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var users = await productService.GetAllProductAsync();
        return users.Select(x => new ProductDTO()
        {
            Name = x.name,
            ManufactureEmail = x.manufactureEmail,
            ManufacturePhone = x.manufacturePhone,
            ProductDate = x.ProductDate
        }).ToList();
    }
}