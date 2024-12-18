using Application.Features.Models.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Base.Application.Pipelines.Transaction;
using Domain.Entites;
using MediatR;
using Contracts.Product;
using Contracts.Model;

namespace Application.Features.Models.Commands.Create;

public class CreateModelCommand : IRequest<CreatedModelResponse>, ITransactionalRequest
{
    public Guid BrandId { get; set; }
    public string Name { get; set; }
    public string ImageUrl { get; set; }
    public class CreateModelCommandHandler : IRequestHandler<CreateModelCommand, CreatedModelResponse>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;
        private readonly ModelBusinessRules _modelBusinessRules;

        public CreateModelCommandHandler(IModelRepository modelRepository, IMapper mapper, ModelBusinessRules modelBusinessRules)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
            _modelBusinessRules = modelBusinessRules;
        }

        public async Task<CreatedModelResponse>? Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {

            await _modelBusinessRules.ModelNameCannotBeDuplicatedWhenInserted(request.Name);

            Model model = _mapper.Map<Model>(request);
            model.Id = Guid.NewGuid();

            await _modelRepository.AddAsync(model);

            CreatedModelResponse createdModelResponse = _mapper.Map<CreatedModelResponse>(model);
            return createdModelResponse;
        }

    }
}