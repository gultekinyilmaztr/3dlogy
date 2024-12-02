using Application.Features.Categories.Commands.Create;
using AutoMapper;
using Domain.Entites;

namespace Application.Features.Categories.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            //CreateMap<Category, CreatedCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryCommand>().ReverseMap();
            CreateMap<Category, CreatedCategoryResponse>().ReverseMap();
            //CreateMap<IPaginate<Category>, CategoryListModel>().ReverseMap();
            //CreateMap<Category, CategoryListDto>().ReverseMap();
            //CreateMap<Category, CategoryGetByIdDto>().ReverseMap();
        }
    }
}
