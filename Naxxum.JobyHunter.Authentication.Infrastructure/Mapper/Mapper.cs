using Authentication.Application.DTOs;
using Authentication.Infra;
using Authentication.Infra.Identity;
using AutoMapper;

namespace Authentication.Application.Mapper;

public class Mapper : Profile
{
    public Mapper()
    {
        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<ApplicationUser, UserResponseDTO>().ReverseMap();
    }
}