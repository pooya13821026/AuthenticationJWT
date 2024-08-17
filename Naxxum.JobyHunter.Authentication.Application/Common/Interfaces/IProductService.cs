using Authentication.Application.DTOs;

namespace Authentication.Application.Common.Interfaces;

public interface IProductService
{
    Task<bool> CreateProductAsync(string name, string userId);
    Task<bool> DeleteProductAsync(int productId, string userId);

    Task<bool> UpdateProduct(int productId, string userId, string name, bool isAvailable);

    Task<List<(int id, string name, string manufactureEmail, string manufacturePhone, DateTime ProductDate)>>
        GetAllProductAsync();
}