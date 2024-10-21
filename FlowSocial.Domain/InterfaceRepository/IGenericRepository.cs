using System.Linq.Expressions;


namespace FlowSocial.Domain.InterfaceRepository
{
    public interface IGenericRepository<T>where T : class
    {
        IQueryable<T?> GetByIdAsync(Expression<Func<T, bool>> filter);
       
        IQueryable<T> GetAllAsync();
        IQueryable<T> FindAsync(Expression<Func<T, bool>> predicate);

        Task<T> AddAsync(T entity);
        Task UpdateAsync();
        Task DeleteAsync(T entity);
    }
}
