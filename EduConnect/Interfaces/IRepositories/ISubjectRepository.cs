using EduConnect.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface ISubjectRepository : IBaseRepository<Subject>
    {
        Task<Subject> GetSubject(Expression<Func<Subject, bool>> expression);
        Task<IList<Subject>> GetAllSubject();
        Task<IList<Subject>> GetSelectedSubjects(IList<int> ids);
        
        
       
      
    }
}
