using Application.Services.Repositories;
using Base.Persistence.Repositories;
using Domain.Entites;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ModelRepository : EfRepositoryBase<Model, Guid, BaseDbContext>, IModelRepository
    {
        public ModelRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
