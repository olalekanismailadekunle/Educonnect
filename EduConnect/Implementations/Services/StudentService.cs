using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Identity;
using EduConnect.Implementations.Repositories;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using System.Linq;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISubjectRepository _subjectRepository;


        public StudentService(IStudentRepository studentRepository, IRoleRepository roleRepository , ISubjectRepository subjectRepository)
        {
            _studentRepository = studentRepository;
            _roleRepository = roleRepository;
            _subjectRepository = subjectRepository;
        }

        public async Task<StudentResponseModel> CreateStudent(StudentRequestModel requestModel)
        {
            var getStudent = await _studentRepository.Get(x => x.FirstName == requestModel.FirstName && x.LastName == requestModel.LastName && x.IsDeleted == false);
            if (getStudent != null)
            {
                return new StudentResponseModel
                {
                    Message = "Student already exist",
                    Status = false
                };
            }

            var studet = new Student
            {
                IsDeleted = false,
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                Level = requestModel.Level,
               ParentId = requestModel.ParentId,
               Deleted = Enums.Deleted.NotDeleted
                
            };
            var address = new Address
            {
                HouseNumber = requestModel.HouseNumber,
                LGAOfResidence = requestModel.LGAOfResidence,
                State = requestModel.State,
                StreetName = requestModel.StreetName,
                Student = studet,
                StudentId = studet.Id
                

            };
            studet.Address = address;
            var subjects = await _subjectRepository.GetSelectedSubjects(requestModel.SubjectIds);
            foreach (var item in subjects)
            {
                var studentSubjesct = new StudentSubject
                {
                    Student = studet,
                    StudentId = studet.Id,
                    Subject = item,
                    SubjectId = item.Id
                };
                   studet.Subjects.Add(studentSubjesct);
            }
            await _studentRepository.Create(studet);
            return new StudentResponseModel
            {
                Data = new StudentDto
                {
                    Address = new AddressDto
                    {
                        State = requestModel.State,
                        LGAOfResidence = requestModel.LGAOfResidence,
                        HouseNumber = requestModel.HouseNumber,
                        StreetName = requestModel.StreetName
                    },
                    FirstName = requestModel.FirstName,
                    LastName = requestModel.LastName,
                    Level = requestModel.Level
                },
                Message = "Student Successfully Created",
                Status = true
            };
        }

        public async Task<StudentResponseModel> DeleteStudent(int Id)
        {
            var student = await _studentRepository.Get(x => x.Id == Id);
            if (student == null)
            {
                return new StudentResponseModel
                {
                    Message = "Student not found",
                    Status = false
                };
            }
            student.IsDeleted = true;
            student.Deleted = Enums.Deleted.Approved;
            await _studentRepository.Update(student);
            return new StudentResponseModel
            {
                Status = true,
                Message = "Student Successfully deleted",
            };
        }

        public async Task<StudentResponseModel> DeleteStudentForParent(int Id)
        {
            var student = await _studentRepository.Get(x => x.Id == Id);
            if (student == null)
            {
                return new StudentResponseModel
                {
                    Message = "Student not found",
                    Status = false
                };
            }
            student.Deleted = Enums.Deleted.Processing;
            await _studentRepository.Update(student);
            return new StudentResponseModel
            {
                Status = true,
                Message = "Student Successfully deleted",
            };
        }

        public async Task<StudentsResponseModel> GetAllStudent()
        {
            var students = await _studentRepository.GetAllStudent();
            if (students.Count == 0)
            {
                return new StudentsResponseModel
                {
                    Status = false,
                    Message = "Student List is empty"
                };
            }
            return new StudentsResponseModel
            {
                Data = students.Select(std => new StudentDto
                {
                    Id = std.Id,
                    FirstName = std.FirstName,
                    LastName = std.LastName,
                    Address = new AddressDto
                    {
                       State = std.Address.State,
                       LGAOfResidence = std.Address.LGAOfResidence,
                       HouseNumber = std.Address.HouseNumber,
                        StreetName = std.Address.StreetName
                    },
                    Level = std.Level
                }).ToList(),
                Status = true,
                Message = "student Successfully Retrieved"
            };
        }

        public async Task<StudentsResponseModel> GetAllStudentForParent(int id)
        {
            var students = await _studentRepository.GetAllStudentForParent(id);
            if (students.Count == 0)
            {
                return new StudentsResponseModel
                {
                    Status = false,
                    Message = "Student List is empty"
                };
            }
            return new StudentsResponseModel
            {
                Data = students.Select(std => new StudentDto
                {
                    Id = std.Id,
                    FirstName = std.FirstName,
                    LastName = std.LastName,
                    Address = new AddressDto
                    {
                        State = std.Address.State,
                        LGAOfResidence = std.Address.LGAOfResidence,
                        HouseNumber = std.Address.HouseNumber,
                        StreetName = std.Address.StreetName
                    },
                    Level = std.Level
                }).ToList(),
                Status = true,
                Message = "student Successfully Retrieved"
            };
        }

        public async Task<StudentsResponseModel> GetAllToBeApprovedDeletedStudent()
        {
            var students = await _studentRepository.GetStudentWaitingForApprovalToBeDeleted();
            if (students.Count == 0)
            {
                return new StudentsResponseModel
                {
                    Status = false,
                    Message = "Student Not Found"
                };
            }
            return new StudentsResponseModel
            {
                Data = students.Select(std => new StudentDto
                {
                    Id = std.Id,
                    FirstName = std.FirstName,
                    LastName = std.LastName,
                    Address = new AddressDto
                    {
                        State = std.Address.State,
                        LGAOfResidence = std.Address.LGAOfResidence,
                        HouseNumber = std.Address.HouseNumber,
                        StreetName = std.Address.StreetName
                    },
                    Level = std.Level
                }).ToList(),
                Status = true,
                Message = "student Successfully Retrieved"
            };
        }

        public async Task<StudentResponseModel> GetStudentById(int id)
        {
            var student = await _studentRepository.GetStudent(x => x.Id == id);
            if (student == null)
            {
                return new StudentResponseModel
                {
                    Status = false,
                    Message = "Student Not Found"
                };
            }
            return new StudentResponseModel
            {
                Data = new StudentDto
                {
                    Address  = new AddressDto
                    {
                        State = student.Address.State,
                        LGAOfResidence = student.Address.LGAOfResidence,
                        HouseNumber = student.Address.HouseNumber,
                        StreetName = student.Address.StreetName
                    },
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Level = student.Level
                },
                Status = true,
                Message = "student Successfully Retrieved"
            };
        }

        public async Task<StudentResponseModel> GetStudentByLocalGovernment(string name)
        {
            var student = await _studentRepository.GetStudent(x => x.Address.LGAOfResidence == name);
            if (student == null)
            {
                return new StudentResponseModel
                {
                    Status = false,
                    Message = "Student Not Found"
                };
            }
            return new StudentResponseModel
            {
                Data = new StudentDto
                {
                    Address = new AddressDto
                    {
                        State = student.Address.State,
                        LGAOfResidence = student.Address.LGAOfResidence,
                        HouseNumber = student.Address.HouseNumber,
                        StreetName = student.Address.StreetName
                    },
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Level = student.Level
                },
                Status = true,
                Message = "student Successfully Retrieved"
            };
        }

        public async Task<StudentResponseModel> GetStudentByState(string name)
        {
            var student = await _studentRepository.GetStudent(x => x.Address.State == name);
            if (student == null)
            {
                return new StudentResponseModel
                {
                    Status = false,
                    Message = "Student Not Found"
                };
            }
            return new StudentResponseModel
            {
                Data = new StudentDto
                {
                    Address = new AddressDto
                    {
                        State = student.Address.State,
                        LGAOfResidence = student.Address.LGAOfResidence,
                        HouseNumber = student.Address.HouseNumber,
                        StreetName = student.Address.StreetName
                    },
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Level = student.Level
                },
                Status = true,
                Message = "student Successfully Retrieved"
            };
        }

        public async Task<StudentResponseModel> UpdateStudent(StudentUpdateModel updateModel, int id)
        {
            var student = await _studentRepository.GetStudent(x => x.Id == id);
            if (student == null)
            {
                return new StudentResponseModel
                {
                    Message = "student not found",
                    Status = false
                };
            }
            if(updateModel.SubjectIds.Count != 0)
            {
                var subjects = await _subjectRepository.GetSelectedSubjects(updateModel.SubjectIds);
                foreach (var item in subjects)
                {
                    var studentSubjesct = new StudentSubject
                    {
                        Student = student,
                        StudentId = student.Id,
                        Subject = item,
                        SubjectId = item.Id
                    };
                    if(!(student.Subjects.Any(a => a.StudentId == studentSubjesct.Id)))
                    {
                        student.Subjects.Add(studentSubjesct);
                    }
                 
                }
            }
            if(updateModel.LGAOfResidence != null && updateModel.State == null && updateModel.HouseNumber == null && updateModel.StreetName == null)
            {
                var adress = new Address
                {
                    State = updateModel.State,
                    LGAOfResidence = updateModel.LGAOfResidence,
                    StreetName = updateModel.StreetName,
                    HouseNumber = updateModel.HouseNumber,
                };
                student.Address = adress;
            }
           
            student.FirstName = updateModel.FirstName ?? student.LastName;
            student.LastName = updateModel.LastName ?? student.LastName;
            
            student.Level = (updateModel.Level.ToString() == null) ? student.Level : updateModel.Level; 
            await _studentRepository.Update(student);
            return new StudentResponseModel
            {
                Status = true,
                Message = "student Successfully Updated",
                Data = new StudentDto
                {
                    Address = new AddressDto
                    {
                        State = student.Address.State,
                        LGAOfResidence = student.Address.LGAOfResidence,
                        HouseNumber = student.Address.HouseNumber,
                        StreetName = student.Address.StreetName
                    },
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Level = student.Level
                },
            };
        }
    }
}
