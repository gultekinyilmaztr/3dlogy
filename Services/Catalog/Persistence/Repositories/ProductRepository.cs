using Application.Services.Repositories;
using Base.Persistence.Repositories;
using Domain.Entites;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ProductRepository : EfRepositoryBase<Product, Guid, BaseDbContext>, IProductRepository
    {
        public ProductRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
