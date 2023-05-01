using EduConnect.Identity;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> ExistsByEmail(string email);

        Task<bool> ExistsById(int id);

        Task<User> GetByEmail(string email);
        Task<User> GetById (int id);

        Task<User> GetUserByEmail(string email);
    }
}
