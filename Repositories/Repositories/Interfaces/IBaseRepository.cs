using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(int? id,T entity);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithRelationsAsync(params Expression<Func<T, object>>[] includeProperties);
        Task<List<T>> GetAllByExpressionAsync(Func<T, bool> predicate);
        Task<T> GetByExpressionAsync(Func<T, bool> predicate);
        Task<List<T>> SearchAsync(Func<T, bool> predicate);
    }
}
