using Base.Persistence.Services.Dynamic;
using Base.Persistence.Services.Paging;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Base.Persistence.Services.Repositories
{
    public interface IAsyncRepository<TEntity, TEntityId> : IQuery<TEntity>
        where TEntity : BaseEntity<TEntityId>
    {
        Task<TEntity?> GetAsync(
            Expression<Func<TEntity, bool>> predicate, //Bir koşul ifadesidir. Örneğin: "Id'si 5 olan ürünü getir" gibi.
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, // İlişkili verileri (örneğin, bir ürünün kategorisi gibi) dahil etmek için kullanılır.
            bool withDeleted = false,//Silinmiş kayıtların getirilip getirilmeyeceğini belirler.
            bool enableTracking = true,//Verinin takip edilip edilmeyeceğini belirtir. (Değişiklikler izlenir mi?)
            CancellationToken cancellationToken = default); //Asenkron işlemi iptal etmek için kullanılır.

        Task<Paginate<TEntity>> GetListAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);

        Task<Paginate<TEntity>> GetListByDynamicsAsync(
            DynamicQuery dynamic,//Arama kriterine göre hangilerini doldurursan ona göre selectquery oluşturulur.
            Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            int index = 0,
            int size = 10,
            bool withDeleted = false,
            bool enableTracking = true,
            CancellationToken cancellationToken = default);

        Task<bool> AnyAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            bool withDeleted = false,
            bool enableTracking = true, CancellationToken cancellationToken = default);

        Task<TEntity> AddAsync(TEntity entity);
        Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities);
        Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false);
        Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false);



    }
}
