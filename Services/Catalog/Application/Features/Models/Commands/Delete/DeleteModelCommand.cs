using Application.Services.Repositories;
using AutoMapper;
using Contracts.Model;
using Domain.Entites;
using MediatR;

namespace Application.Features.Models.Commands.Delete;

public class DeleteModelCommand : IRequest<DeletedModelResponse>
{
    public Guid Id { get; set; }

    public class DeleteModelCommandHandler : IRequestHandler<DeleteModelCommand, DeletedModelResponse>
    {
        private readonly IModelRepository _modelRepository;
        private readonly IMapper _mapper;

        public DeleteModelCommandHandler(IModelRepository modelRepository, IMapper mapper)
        {
            _modelRepository = modelRepository;
            _mapper = mapper;
        }
        public async Task<DeletedModelResponse> Handle(DeleteModelCommand request, CancellationToken cancellationToken)
        {
            Model? model = await _modelRepository.GetAsync(predicate: b => b.Id == request.Id, cancellationToken: cancellationToken);



            await _modelRepository.DeleteAsync(model);

            DeletedModelResponse response = _mapper.Map<DeletedModelResponse>(model);

            return response;
        }
    }
}
