using AutoMapper;
using Contracts.Product;
using Filter.WebApi.Models;

namespace Filter.WebApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatedProductResponse, Product>().ReverseMap();
            CreateMap<UpdatedProductResponse, Product>().ReverseMap();
        }
    }
}
