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
    public class ParentRepository : BaseRepository<Parent>, IParentRepository
    {
        public ParentRepository(ApplicationContext context)
        {
            _context = context;
        }
    
public async Task<IList<Parent>> GetAllParent()
        {
            return await _context.Parents.Include(x => x.User).Include(a => a.Address).Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<Parent> GetParent(Expression<Func<Parent, bool>> expression)
        {
            return await _context.Parents.Include(x => x.User).Include(a => a.Address).SingleOrDefaultAsync(expression);
        }

        public async Task<Parent> GetParentByIdAsync(int id)
        {
            return await _context.Parents.FindAsync(id);
        }

        public Task<IList<Parent>> GetParentByLGA()
        {
            throw new NotImplementedException();
        }

        Parent IParentRepository.GetParentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
