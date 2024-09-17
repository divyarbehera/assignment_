using ApiTest.Contracts.Database;
using ApiTest.Contracts.DTO;
using AutoMapper;
public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CreateProductRequsestDTO, Product>();
    }
}