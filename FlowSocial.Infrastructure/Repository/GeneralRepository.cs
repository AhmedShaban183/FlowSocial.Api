


using FlowSocial.Domain.InterfaceRepository;
using FlowSocial.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FlowSocial.Infrastructure.Repository
{
    public class GeneralRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FlowSocialContext Context;

        public GeneralRepository(FlowSocialContext context)
        {
            Context = context;
        }

        
        public async Task<T> AddAsync(T entity)
        {
            await Context.Set<T>().AddAsync(entity);
            var x=await Context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            Context.Set<T>().Remove(entity);
            await Context.SaveChangesAsync();
        }

        public IQueryable<T> FindAsync(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Context.Set<T>().Where(predicate).AsQueryable();
        }

        public IQueryable<T> GetAllAsync()
        {
            return Context.Set<T>().AsQueryable();
        }

        public   IQueryable<T?> GetByIdAsync( Expression<Func<T, bool>> filter)
        {

            return  Context.Set<T>().Where(filter);
        }

     

        public async Task UpdateAsync()
        {
           // Context.Set<T>().Update(entity);
           
            await Context.SaveChangesAsync();
        }
    }
}
