using EduConnect.Contract;
using EduConnect.Identity;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity, new()
    {
        Task<T> Get(Expression<Func<T, bool>> expression);

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<T> Get(int id);

        Task<IEnumerable<T>> GetAll();

        Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> expression);

        Task<bool> Exists(Expression<Func<User, bool>> expression);

        Task<bool> Exist(int id);

        Task<int> SaveChanges();
    }
}
