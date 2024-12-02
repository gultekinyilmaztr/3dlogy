using Application.Services.Repositories;
using Base.Persistence.Repositories;
using Domain.Entites;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class BrandRepository : EfRepositoryBase<Brand, Guid, BaseDbContext>, IBrandRepository
    {
        public BrandRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
