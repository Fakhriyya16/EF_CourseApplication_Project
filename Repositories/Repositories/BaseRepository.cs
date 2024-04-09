using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Repositories.Data;
using Repositories.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly AppDbContext<T> _appDbContext;
        public BaseRepository()
        {
            _appDbContext = new AppDbContext<T>();
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

        public async Task<List<T>> GetAll()
        {
           return await _appDbContext.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllByExpression(Func<T, bool> predicate)
        {
            var datas = await _appDbContext.Set<T>().ToListAsync();
            var result = datas.Where(predicate).ToList();
            return result;
        }

        public async Task<T> GetByExpression(Func<T, bool> predicate)
        {
            var datas = await _appDbContext.Set<T>().ToListAsync();
            var result = datas.FirstOrDefault(predicate);
            return result;
        }

        public async Task<List<T>> Search(Func<T, bool> predicate)
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
