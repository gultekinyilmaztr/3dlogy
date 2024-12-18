using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Queries.GetById;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetListByDynamic;
using AutoMapper;
using Base.Application.Responses;
using Base.Persistence.Paging;
using Contracts.Product;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Entites;

namespace Application.Features.Products.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, CreatedProductResponse>().ReverseMap();

            CreateMap<Product, UpdateProductCommand>().ReverseMap();
            CreateMap<Product, UpdatedProductResponse>().ReverseMap();

            CreateMap<Product, DeleteProductCommand>().ReverseMap();
            CreateMap<Product, DeletedProductResponse>().ReverseMap();

            CreateMap<Product, GetByIdProductResponse>().ReverseMap();//??



            CreateMap<Product, GetListProductListItemDto>()//brandıd yerine name geliyor normalde..
                .ForMember(destinationMember: c => c.Id, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
                .ReverseMap();
            CreateMap<Product, GetListProductListItemDto>()
               .ForMember(destinationMember: c => c.BrandId, memberOptions: opt => opt.MapFrom(c => c.Model.Name))
               .ReverseMap();
            CreateMap<Product, GetListProductListItemDto>()
               .ForMember(destinationMember: c => c.SubCategoryId, memberOptions: opt => opt.MapFrom(c => c.SubCategory.SubCategoryName))
               .ReverseMap();


            CreateMap<Product, GetListByDynamicProductListItemDto>()//brandıd yerine name geliyor normalde..
                .ForMember(destinationMember: c => c.BrandId, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
                .ReverseMap();
            CreateMap<Product, GetListByDynamicProductListItemDto>()
               .ForMember(destinationMember: c => c.ModelId, memberOptions: opt => opt.MapFrom(c => c.Model.Name))
               .ReverseMap();
            CreateMap<Product, GetListByDynamicProductListItemDto>()
               .ForMember(destinationMember: c => c.SubCategoryId, memberOptions: opt => opt.MapFrom(c => c.SubCategory.SubCategoryName))
               .ReverseMap();


            CreateMap<Paginate<Product>, GetListResponse<GetListProductListItemDto>>().ReverseMap();
            CreateMap<Paginate<Product>, GetListResponse<GetListByDynamicProductListItemDto>>().ReverseMap();
        }
    }
}
