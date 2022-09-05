using Mc2.CrudTest.Application.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataContext _dataContext;
        public GenericRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<T> Add(T entity)
        {
            await _dataContext.AddAsync(entity);
            return entity;
        }

        public async Task Delete(T entity)
        {
             _dataContext.Set<T>().Remove(entity);
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await _dataContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dataContext.Set<T>().ToListAsync();
        }

        public Task Save()
        {
            throw new NotImplementedException();
        }

        public async Task Update(T entity)
        {
            _dataContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
