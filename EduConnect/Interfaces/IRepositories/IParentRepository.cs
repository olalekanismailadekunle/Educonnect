using EduConnect.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IParentRepository : IBaseRepository<Parent>
    {
        Task<Parent> GetParent(Expression<Func<Parent, bool>> expression);
        Task<IList<Parent>> GetAllParent();
        Parent GetParentByIdAsync(int id);
        Task<IList<Parent>> GetParentByLGA();
    }
}
