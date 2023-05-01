using EduConnect.Context;
using EduConnect.Entities;
using EduConnect.Enums;
using EduConnect.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Repositories
{
    public class TutorRepository : BaseRepository<Tutor>, ITutorRepository
    {
        public TutorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IList<Tutor>> GetAllTutor()
        {
            return await _context.Tutors.Include(a => a.Address).Where(x => x.IsDeleted == false).ToListAsync();
        }

        public async Task<IList<Tutor>> GetAllTutorBySpecialization(Specialization specialization)
        {
            var spec = await _context.Tutors.Include(a => a.Address).Where(x => x.Specialization == specialization).ToListAsync();
            return spec;
        }

        public async Task<Tutor> GetTutor(Expression<Func<Tutor, bool>> expression)
        {
            return await _context.Tutors.Include(a  => a.Address).Include(a => a.DocumentImages).Include(a => a.SubjectTutor).ThenInclude(a => a.Subject).SingleOrDefaultAsync(expression);
        } 

        public async Task<IList<Tutor>> GetTutorsByLocalGovernment(string name)
        {
            var tutors = await _context.Tutors.Include(b => b.Address).Where(x => x.Address.LGAOfResidence == name).ToListAsync();
            return tutors;
        }

        public async Task<IList<Tutor>> GetTutorByQualification(Qualification qualification)
        {

            var qual = await _context.Tutors.Include(a => a.Address).Where(x => x.Qualification == qualification).ToListAsync();
            return qual;
        }

        public async Task<bool> TutorIsAvailable(int TutorId, int BookingHour)
        {
            var tutor = await _context.Tutors.FindAsync(TutorId);
            if (tutor.WorkinHoursPerday !>= WorkingHours.Hour1)
            {
                return false;
            }
            else
            {
                if ((int)tutor.WorkinHoursPerday - BookingHour !> 0)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<IList<Tutor>> GetTutorsByState(string name)
        {
            var tutors = await _context.Tutors.Include(b => b.Address).Where(x => x.Address.State == name).ToListAsync();
            return tutors;
        }

        public async Task<IList<Tutor>> GetAllTutorByStatus(Status status)
        {

            var qual = await _context.Tutors.Include(a => a.Address).Where(x => x.Status == status).ToListAsync();
            return qual;
        }

        public async Task<IList<Tutor>> GetAllTutorDependingOnSubject(Expression<Func<Tutor, bool>> expression)
        {
            var qual = await _context.Tutors.Include(a => a.Address).Include(a => a.SubjectTutor).ThenInclude(a => a.Subject).Where(expression).ToListAsync();
            return qual;
        }
    }
}
