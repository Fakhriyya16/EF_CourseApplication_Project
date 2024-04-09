using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task<T> UpdateAsync(int? id,T entity);
        Task<List<T>> GetAll();
        Task<List<T>> GetAllByExpression(Func<T, bool> predicate);
        Task<T> GetByExpression(Func<T, bool> predicate);
        Task<List<T>> Search(Func<T, bool> predicate);
    }
}
