using G_Task.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
namespace G_Task.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly G_TaskDbContext _context;

        protected readonly DbSet<T> dbSet;
        public GenericRepository(G_TaskDbContext context) { 
            _context = context;

        }  
      
        public async Task<T> Add(T entity)
        {
           await _context.AddAsync(entity);

            _context.SaveChanges();

            return entity;
        }

        public async Task<bool> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(long id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            return entity;
        }

        public async Task<bool> IsExist(long id)
        {
            var entity = await GetAsync(id);

            return entity != null;
        }

        public async Task<bool> Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
