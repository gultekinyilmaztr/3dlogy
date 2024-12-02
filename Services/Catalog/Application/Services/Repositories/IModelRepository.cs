using Base.Persistence.Repositories;
using Domain.Entites;

namespace Application.Services.Repositories
{
    public interface IModelRepository : IAsyncRepository<Model, Guid>
    {
    }
}
