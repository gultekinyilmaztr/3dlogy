using Base.Persistence.Dynamic;
using Base.Persistence.Paging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Linq.Expressions;
using System.Reflection;

namespace Base.Persistence.Repositories
{
    public class EfRepositoryBase<TEntity, TEntityId, TContext>
        : IAsyncRepository<TEntity, TEntityId>
        where TEntity : BaseEntity<TEntityId>
        where TContext : DbContext
    {
        protected readonly TContext _context;

        public EfRepositoryBase(TContext context)
        {
            _context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> AddRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.CreatedDate = DateTime.UtcNow;
            await _context.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;

        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.AnyAsync(cancellationToken);
        }

        public async Task<TEntity> DeleteAsync(TEntity entity, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entity, permanent);//nesne silinecek mi yok sa güncellenecek mi karar veriyor.
            await _context.SaveChangesAsync(permanent);
            return entity;
        }

        public async Task<ICollection<TEntity>> DeleteRangeAsync(ICollection<TEntity> entities, bool permanent = false)
        {
            await SetEntityAsDeletedAsync(entities, permanent);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public async Task<Paginate<TEntity>> GetListAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query();
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            if (orderBy != null)
                return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken);
            return await queryable.ToPaginateAsync(index, size, cancellationToken);
        }

        public async Task<Paginate<TEntity>> GetListByDynamicsAsync(DynamicQuery dynamic, Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = Query().ToDynamic(dynamic);
            if (!enableTracking)
                queryable = queryable.AsNoTracking();
            if (include != null)
                queryable = include(queryable);
            if (withDeleted)
                queryable = queryable.IgnoreQueryFilters();
            if (predicate != null)
                queryable = queryable.Where(predicate);
            return await queryable.ToPaginateAsync(index, size, cancellationToken);
        }

        public IQueryable<TEntity> Query() => _context.Set<TEntity>();

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            _context.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<TEntity>> UpdateRangeAsync(ICollection<TEntity> entities)
        {
            foreach (TEntity entity in entities)
                entity.UpdatedDate = DateTime.UtcNow;
            _context.UpdateRange(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        protected async Task SetEntityAsDeletedAsync(TEntity entity, bool permanent)//yardımcı method.
        {
            if (!permanent)
            {
                CheckHasEntityHaveOneToOneRelation(entity);
                await setEntityAsSoftDeletedAsync(entity);
            }
            else
            {
                _context.Remove(entity);
            }
        }

        private async Task setEntityAsSoftDeletedAsync(IEntityTimestamps entity)
        {
            if (entity.DeletedDate.HasValue)
                return; // Zaten silinmiş

            entity.DeletedDate = DateTime.UtcNow;

            var navigations = _context
                .Entry(entity)
                .Metadata.GetNavigations()
                .Where(x => x is { IsOnDependent: false, ForeignKey.DeleteBehavior: DeleteBehavior.ClientCascade or DeleteBehavior.Cascade })
                .ToList();

            foreach (INavigation? navigation in navigations)
            {
                if (navigation.TargetEntityType.IsOwned() || navigation.PropertyInfo == null)
                    continue;

                object? navValue = navigation.PropertyInfo?.GetValue(entity);
                if (navValue == null)
                {
                    IQueryable query = _context.Entry(entity)
                                                .Collection(navigation.PropertyInfo.Name)
                                                .Query();
                    navValue = await GetRelationLoaderQuery(query, navigation.PropertyInfo.PropertyType).ToListAsync();
                }

                if (navValue is null)
                    continue;

                if (navigation.IsCollection)
                {
                    foreach (var navValueItem in (IEnumerable)navValue)
                    {
                        if (navValueItem is IEntityTimestamps item)
                            await setEntityAsSoftDeletedAsync(item);
                    }
                }
                else if (navValue is IEntityTimestamps singleEntity)
                {
                    await setEntityAsSoftDeletedAsync(singleEntity);
                }
            }

            _context.Update(entity);
        }


        protected IQueryable<object> GetRelationLoaderQuery(IQueryable query, Type navigationPropertyType)//Bir Product nesnesinin Reviews (yorumlar) ilişkisi var.Bu metod, yalnızca DeletedDate alanı boş olan (silinmemiş) yorumları getirir.Böylece, ilişkili veriler üzerinde "soft delete" işlemi düzgün çalışır.
        {
            Type queryProviderType = query.Provider.GetType();
            MethodInfo createQueryMethod =
                queryProviderType
                    .GetMethods()
                    .First(m => m is { Name: nameof(query.Provider.CreateQuery), IsGenericMethod: true })
                    ?.MakeGenericMethod(navigationPropertyType)
                ?? throw new InvalidOperationException("CreateQuery<TElement> method is not found in IQueryProvider.");
            var queryProviderQuery =
                (IQueryable<object>)createQueryMethod.Invoke(query.Provider, parameters: new object[] { query.Expression })!;
            return queryProviderQuery.Where(x => !((IEntityTimestamps)x).DeletedDate.HasValue);
        }

        protected void CheckHasEntityHaveOneToOneRelation(TEntity entity)//bire bir (one-to-one) ilişkiye sahip olup olmadığını kontrol et. exception fırlat.
        {
            bool hasEntityHaveOneToOneRelation =
                _context
                .Entry(entity)
                .Metadata.GetForeignKeys()
                .All(
                    x =>
                    x.DependentToPrincipal?.IsCollection == true
                    || x.PrincipalToDependent?.IsCollection == true
                    || x.DependentToPrincipal?.ForeignKey.DeclaringEntityType.ClrType == entity.GetType()
                    ) == false;
            if (hasEntityHaveOneToOneRelation)
                throw new NotImplementedException(
                    "Entity has one-to-one relationship. Soft Delete causes problem if you try to create entry again by same foreing key"
                    );
        }

        protected async Task SetEntityAsDeletedAsync(IEnumerable<TEntity> entities, bool permanent)
        {
            foreach (TEntity entity in entities)
                await SetEntityAsDeletedAsync(entity, permanent);
        }

    }
}
