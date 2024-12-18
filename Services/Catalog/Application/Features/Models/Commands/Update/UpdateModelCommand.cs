using Application.Services.Repositories;
using AutoMapper;
using Contracts.Model;
using Domain.Entites;
using MediatR;

namespace Application.Features.Models.Commands.Update;

public class UpdateModelCommand : IRequest<UpdatedModelResponse>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ModelId { get; set; }
    public string ImageUrl { get; set; }

    public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand, UpdatedModelResponse>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public UpdateModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task<UpdatedModelResponse> Handle(UpdateModelCommand request, CancellationToken cancellationToken)
        {
            Model? model = await _modelRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);

            model = _mapper.Map(request, model);

            await _modelRepository.UpdateAsync(model);

            UpdatedModelResponse response = _mapper.Map<UpdatedModelResponse>(model);

            return response;
        }
    }
}
