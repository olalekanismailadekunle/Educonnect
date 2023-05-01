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
    public class AdministratorRepository : BaseRepository<Administrator>,IAdministratorRepository
    {
        public AdministratorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Administrator> GetAdministrator(Expression<Func<Administrator, bool>> expression)
        {
            return await _context.Administrators.Include(x => x.User).SingleOrDefaultAsync(expression);
        }

        

        public async Task<IList<Administrator>> GetAllAdministrators()
        {
            return await _context.Administrators.Include(x => x.User).Where(x => x.IsDeleted == false).ToListAsync();
        }
    }
}
