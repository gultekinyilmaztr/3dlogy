using Application.Features.Models.Commands.Create;
using Application.Features.Models.Commands.Delete;
using Application.Features.Models.Commands.Update;
using Application.Features.Models.Queries.GetList;
using Application.Features.Models.Queries.GetListByDynamic;
using AutoMapper;
using Base.Application.Responses;
using Base.Persistence.Paging;
using Domain.Entites;

namespace Application.Features.Models.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {

            CreateMap<Model, CreateModelCommand>().ReverseMap();
            CreateMap<Model, CreatedModelResponse>().ReverseMap();

            CreateMap<Model, UpdateModelCommand>().ReverseMap();
            CreateMap<Model, UpdatedModelResponse>().ReverseMap();

            CreateMap<Model, DeleteModelCommand>().ReverseMap();
            CreateMap<Model, DeletedModelResponse>().ReverseMap();


            CreateMap<Model, GetListModelListItemDto>()
                .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
                .ReverseMap();
            CreateMap<Model, GetListByDynamicModelListItemDto>()
                .ForMember(destinationMember: c => c.BrandName, memberOptions: opt => opt.MapFrom(c => c.Brand.Name))
                .ReverseMap();
            CreateMap<Paginate<Model>, GetListResponse<GetListModelListItemDto>>().ReverseMap();
            CreateMap<Paginate<Model>, GetListResponse<GetListByDynamicModelListItemDto>>().ReverseMap();
        }
    }
}
