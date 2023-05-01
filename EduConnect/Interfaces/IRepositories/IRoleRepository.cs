using EduConnect.Identity;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<bool> ExistsByName(string name);

        Task<bool> ExistsById(int id);

        Task<Role> GetByName(string name);
    }
}
