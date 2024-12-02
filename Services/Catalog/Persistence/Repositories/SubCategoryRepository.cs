using Application.Services.Repositories;
using Base.Persistence.Repositories;
using Domain.Entites;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class SubCategoryRepository : EfRepositoryBase<SubCategory, Guid, BaseDbContext>, ISubCategoryRepository
    {
        public SubCategoryRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
