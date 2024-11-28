using Base.Persistence.Services.Repositories;
using Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories
{
    public interface ICategoryRepository : IAsyncRepository<Category,Guid>
    {
    }
}
