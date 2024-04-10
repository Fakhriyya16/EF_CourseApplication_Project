using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        public BaseRepository()
        {
            _appDbContext = new AppDbContext();
        }
        public async Task CreateAsync(T entity)
        {
            await _appDbContext.Set<T>().AddAsync(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _appDbContext.Set<T>().Remove(entity);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
           return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllByExpressionAsync(Func<T, bool> predicate)
        {
            var datas = await _appDbContext.Set<T>().ToListAsync();
            var result = datas.Where(predicate).ToList();
            return result;
        }

        public async Task<List<T>> GetAllWithRelationsAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _appDbContext.Set<T>();

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetByExpressionAsync(Func<T, bool> predicate)
        {
            var datas = await _appDbContext.Set<T>().ToListAsync();
            var result = datas.FirstOrDefault(predicate);
            return result;
        }

        public async Task<List<T>> SearchAsync(Func<T, bool> predicate)
        {
            var datas = await _appDbContext.Set<T>().ToListAsync();
            var result = datas.Where(predicate).ToList();
            return result;
        }

        public async Task<T> UpdateAsync(int? id, T entity)
        {
            var datas = await _appDbContext.Set<T>().ToListAsync();
            var result = datas.FirstOrDefault(m=>m.Id == id);
            return result;
        }
    }
}
