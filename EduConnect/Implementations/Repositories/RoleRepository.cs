﻿using EduConnect.Context;
using EduConnect.Identity;
using EduConnect.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }
    
public async Task<bool> ExistsById(int id)
        {
            return await _context.Roles.AnyAsync(d => d.Id == id);
        }

        public async Task<bool> ExistsByName(string name)
        {
            return await _context.Roles.AnyAsync(n => n.Name.Equals(name));
        }

        public async Task<Role> GetByName(string name)
        {
            return await _context.Roles.FirstOrDefaultAsync(b => b.Name.Equals(name) && b.IsDeleted == false);
        }
    }
}
