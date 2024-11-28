namespace Application.Services.Repositories
{
    public interface IQuery<T>//dapper gibi querylerle sorgu desteği
    {
        IQueryable<T> Query();
    }
}
