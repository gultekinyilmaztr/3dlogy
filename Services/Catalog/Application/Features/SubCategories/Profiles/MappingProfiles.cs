using Application.Features.SubCategories.Commands.Create;
using Application.Features.SubCategories.Commands.Delete;
using Application.Features.SubCategories.Commands.Update;
using Application.Features.SubCategories.Queries.GetById;
using Application.Features.SubCategories.Queries.GetList;
using AutoMapper;
using Base.Application.Responses;
using Base.Persistence.Paging;
using Domain.Entites;

namespace Application.Features.SubCategories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<SubCategory, CreateSubCategoryCommand>().ReverseMap();
            CreateMap<SubCategory, CreatedSubCategoryResponse>().ReverseMap();

            CreateMap<SubCategory, UpdateSubCategoryCommand>().ReverseMap();
            CreateMap<SubCategory, UpdatedSubCategoryResponse>().ReverseMap();

            CreateMap<SubCategory, DeleteSubCategoryCommand>().ReverseMap();
            CreateMap<SubCategory, DeletedSubCategoryResponse>().ReverseMap();

            CreateMap<SubCategory, GetListSubCategoryListItemDto>().ReverseMap();
            CreateMap<SubCategory, GetByIdSubCategoryResponse>().ReverseMap();
            CreateMap<Paginate<SubCategory>, GetListResponse<GetListSubCategoryListItemDto>>().ReverseMap();
        }
    }
}
