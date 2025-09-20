using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace G_Task.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IReadOnlyList<T>> GetAllAsync(); 
        Task<T> GetAsync(long id);
        Task<T> Add(T entity);
        Task<bool> Update(T entity);
        Task<bool> Delete(T entity);
        Task<bool> IsExist(long id);

    }
}
