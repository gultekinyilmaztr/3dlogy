using Application.Services.Repositories;
using AutoMapper;
using Domain.Entites;
using MediatR;

namespace Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommand : IRequest<UpdatedCategoryResponse>
    {
        public Guid Id { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPhotoUrl { get; set; }
        public string IconUrl { get; set; }
    }
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryResponse>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<UpdatedCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category? Category = await _categoryRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            Category = _mapper.Map(request, Category);

            await _categoryRepository.UpdateAsync(Category);

            UpdatedCategoryResponse response = _mapper.Map<UpdatedCategoryResponse>(Category);

            return response;
        }
    }
}
