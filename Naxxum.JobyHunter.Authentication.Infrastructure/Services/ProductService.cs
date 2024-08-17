using Authentication.Application.Common.Exceptions;
using Authentication.Application.Common.Interfaces;
using Authentication.Application.DTOs;
using Authentication.Infra.Data;
using Authentication.Infra.Identity;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infra.Services;

public class ProductService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, IMapper mapper)
    : IProductService
{
    public async Task<bool> CreateProductAsync(string name, string ussrId)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(f => f.Id == ussrId);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }

        var item = new Product()
        {
            UserId = ussrId,
            Name = name,
            ManufactureEmail = user.Email,
            ManufacturePhone = user.PhoneNumber,
            ProductDate = DateTime.Now,
            IsAvailable = true
        };

        await dbContext.Products.AddAsync(item);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProductAsync(int productId, string userId)
    {
        var product = await dbContext.Products.Where(x => x.Id == productId)
            .Include(x => x.User).FirstOrDefaultAsync();
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<bool> UpdateProduct(int productId, string userId, string name, bool isAvailable)
    {
        var product = await dbContext.Products.Where(x => x.Id == productId)
            .Include(x => x.User).FirstOrDefaultAsync();
        if (product == null)
        {
            throw new NotFoundException("Product not found");
        }

        product.Name = name;
        product.IsAvailable = isAvailable;

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<List<(int id, string name, string manufactureEmail, string manufacturePhone,
        DateTime ProductDate)>> GetAllProductAsync()
    {
        var products = await dbContext.Products.Where(w => w.IsAvailable == true).ToListAsync();

        var res = products.Select(products =>
            (products.Id, products.Name, products.ManufactureEmail, products.ManufacturePhone,
                products.ProductDate)).ToList();
        return res;
    }
}