using Base.Persistence.Repositories;
using Domain.Entites;

namespace Application.Services.Repositories
{
    public interface ISubCategoryRepository : IAsyncRepository<SubCategory, Guid>
    {
    }
}
