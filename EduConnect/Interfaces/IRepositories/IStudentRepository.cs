using EduConnect.Entities;
using EduConnect.Enums;
using EduConnect.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public interface IStudentRepository : IBaseRepository<Student>
    {
        Task<Student> GetStudent(Expression<Func<Student, bool>> expression);
        Task<IList<Student>> GetAllStudent();
        Task<IList<Student>> GetAllStudentForParent(int id);
        Student GetStudentByIdAsync(int id);
        Task<Tutor> GetTutor(Expression<Func<Tutor, bool>> expression);
        Task<IList<Student>> GetStudentByLGA();
        Task<IList<Student>> GetStudentByLevel(Level level);
        Task<IList<Student>> GetStudentWaitingForApprovalToBeDeleted();

    }
}
