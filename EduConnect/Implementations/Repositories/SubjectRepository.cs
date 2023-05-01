using EduConnect.Context;
using EduConnect.Entities;
using EduConnect.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class SubjectRepository : BaseRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationContext context)
        {
            _context = context;
        }
    
public async Task<IList<Subject>> GetAllSubject()
        {
            return await _context.Subjects.Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Subject>> GetSelectedSubjects(IList<int> ids)
        {
            return await _context.Subjects.Where(a => ids.Contains(a.Id)).ToListAsync();
        }

        public async Task<Subject> GetSubject(Expression<Func<Subject, bool>> expression)
        {
            return await _context.Subjects.SingleOrDefaultAsync(expression);
        }

       

       
    }
}
