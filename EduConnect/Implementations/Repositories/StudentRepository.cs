using EduConnect.Context;
using EduConnect.Entities;
using EduConnect.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
       
        public StudentRepository(ApplicationContext context)
        {
            _context = context;
        }
    
        public async Task<IList<Student>> GetAllStudent()
        {
            var x =  await _context.Students.Include(x => x.Tutors).Include(a => a.Address).Where(x => x.IsDeleted == false).ToListAsync();
            return x;
        }

        public async Task<Student> GetStudent(Expression<Func<Student, bool>> expression)
        {

            return await _context.Students.Include(x => x.Tutors).Include(a => a.Address).Include(a => a.Subjects).ThenInclude(a => a.Subject).SingleOrDefaultAsync(expression);
        }


        public Task<IList<Student>> GetStudentByLGA()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Student>> GetStudentByLevel( Level level)
        {
            var qual = await _context.Students.Where(x => x.Level == level).ToListAsync();
            return qual;
        }

        public Task<Tutor> GetTutor(Expression<Func<Tutor, bool>> expression)
        {
            throw new NotImplementedException();
        }

        Student IStudentRepository.GetStudentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<Student>> GetAllStudentForParent(int id)
        {
            return await _context.Students.Include(x => x.Tutors).Include(a => a.Address).Where(x => x.IsDeleted == false && x.ParentId == id && x.Deleted != Deleted.Processing).ToListAsync();
        }

        public async Task<IList<Student>> GetStudentWaitingForApprovalToBeDeleted()
        {
            var qual = await _context.Students.Include(x => x.Tutors).Include(a => a.Address).Where(x => x.Deleted == Enums.Deleted.Processing).ToListAsync();
            return qual;
        }
    }
}
