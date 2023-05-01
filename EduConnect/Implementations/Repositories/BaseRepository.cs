using EduConnect.Context;
using EduConnect.Contract;
using EduConnect.Identity;
using EduConnect.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : AuditableEntity, new()
    {
        protected ApplicationContext _context;
        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }


        public async Task<bool> Exists(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.AnyAsync(expression);
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Set<T>().AnyAsync(d => d.Id == id);
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(a => a.Id == id && a.IsDeleted == false);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await _context.Set<T>().Where(expression).ToListAsync();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
