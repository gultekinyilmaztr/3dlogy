using Application.Services.Repositories;
using Base.Persistence.Repositories;
using Domain.Entites;
using Persistence.Context;

namespace Persistence.Repositories
{
    public class ProductDetailRepository : EfRepositoryBase<ProductDetail, Guid, BaseDbContext>, IProductDetailRepository
    {
        public ProductDetailRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
