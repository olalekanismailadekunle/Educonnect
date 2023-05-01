using EduConnect.Context;
using EduConnect.Identity;
using EduConnect.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            return await _context.Users.AnyAsync(d => d.Email.Equals(email) && d.IsDeleted == false);
        }

        public async Task<bool> ExistsById(int id)
        {
            return await _context.Users.AnyAsync(s => s.Id == id && s.IsDeleted == false);
        }

        public async Task<User> GetByEmail(string email)
        {
            return await _context.Users.Include(r => r.UserRoles).ThenInclude(r => r.Role).FirstOrDefaultAsync(b => b.Email.Equals(email) && b.IsDeleted == false);
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.Include(r => r.UserRoles).ThenInclude(r => r.Role).Include(a => a.Administrator).Include(a => a.Tutor).Include(a => a.Parent).FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(d => d.Email.Equals(email) && d.IsDeleted == false).FirstOrDefaultAsync();
        }
    }
}
