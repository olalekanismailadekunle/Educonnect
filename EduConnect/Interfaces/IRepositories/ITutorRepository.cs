using EduConnect.Entities;
using EduConnect.Enums;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduConnect.Interfaces.IRepositories
{
    public interface ITutorRepository : IBaseRepository<Tutor>
    {
        Task<Tutor> GetTutor(Expression<Func<Tutor, bool>> expression);
        Task<IList<Tutor>> GetAllTutor();
        Task<IList<Tutor>> GetAllTutorDependingOnSubject(Expression<Func<Tutor,bool>> expression);
        Task<IList<Tutor>> GetTutorsByLocalGovernment(string name);
        Task<IList<Tutor>> GetTutorsByState(string name); 
        Task<IList<Tutor>> GetTutorByQualification( Qualification qualification);
        Task<IList<Tutor>> GetAllTutorBySpecialization(Specialization specialization);
        Task<IList<Tutor>> GetAllTutorByStatus(Status status);
        Task<bool> TutorIsAvailable(int TutorId, int WorkingHour);
        
    }
}
