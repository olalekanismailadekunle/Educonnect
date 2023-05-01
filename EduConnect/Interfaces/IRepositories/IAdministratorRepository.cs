using EduConnect.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IAdministratorRepository : IBaseRepository<Administrator>
    {
        Task<Administrator> GetAdministrator(Expression<Func<Administrator, bool>> expression);
        Task<IList<Administrator>> GetAllAdministrators();
    }
}
