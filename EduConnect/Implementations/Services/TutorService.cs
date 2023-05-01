using EduConnect.DTOs;
using EduConnect.Entities;
using EduConnect.Enums;
using EduConnect.Identity;
using EduConnect.Implementations.Repositories;
using EduConnect.Interfaces.IRepositories;
using EduConnect.Interfaces.IServices;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace EduConnect.Implementations.Services
{
   public class TutorService : ITutorService
   {
        private readonly ITutorRepository _tutorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IWebHostEnvironment _webpath;
        private readonly IStudentRepository _studentRepository;


        public TutorService(ITutorRepository tutorRepository, IWebHostEnvironment webpath,IUserRepository userRepository, IRoleRepository roleRepository , 
            ISubjectRepository subjectRepository , IStudentRepository studentRepository)
        {
            _tutorRepository = tutorRepository;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _subjectRepository = subjectRepository;
            _webpath = webpath;
            _studentRepository = studentRepository;


        }

        public async Task<TutorResponseModel> CreateTutor(TutorRequestModel requestModel)
        {
            var getTutor = await _tutorRepository.Exists(x => x.Email == requestModel.MailAddress);
            if (getTutor == true)
            {
                return new TutorResponseModel
                {
                    Message = "Tutor already exist",
                    Status = false
                };
            }
            var myImageName = "";
            if (requestModel.ProfilePicture != null)
            {
                var imgpath = _webpath.WebRootPath;
                var imagePath = Path.Combine(imgpath, "myImages");
                Directory.CreateDirectory(imagePath);
                var imageType = requestModel.ProfilePicture.ContentType.Split('/')[1];
                if (imageType == "jpg" || imageType == "png" || imageType == "jpeg")
                {
                    myImageName = $"{Guid.NewGuid()}.{imageType}";
                    var fullpath = Path.Combine(imagePath, myImageName);
                    using (var filestream = new FileStream(fullpath, FileMode.Create))
                    {
                        requestModel.ProfilePicture.CopyTo(filestream);
                    }
                }
                else
                {
                    throw new Exception("Unsorported format");
                }

            }


            var user = new User
            {
                Email = requestModel.MailAddress,
                UserName = requestModel.FirstName,
                Password = BCrypt.Net.BCrypt.HashPassword(requestModel.Password),
                IsDeleted = false,
            };
            var role = await _roleRepository.Get(x => x.Name == "Tutor");
            var userRole = new UserRole
            {
                User = user,
                UserId = user.Id,
                Role = role,
                RoleId = role.Id
            };
            user.UserRoles.Add(userRole);


            var tutor = new Tutor
            {
                FirstName = requestModel.FirstName,
                LastName = requestModel.LastName,
                ProfilePicture = myImageName,
                User = user,
                UserId = user.Id,

                IsDeleted = false,
                WorkinHoursPerday = requestModel.WorkinHoursPerday,
                City = requestModel.City,
                
                MailAddress = requestModel.MailAddress,
                Qualification = requestModel.Qualification,
                Specialization = requestModel.Specialization,
                Status = Status.NotEngaged
            };
            var adress = new Address
            {
                HouseNumber = requestModel.HouseNumber,
                LGAOfResidence = requestModel.LGAOfResidence,
                State = requestModel.State,
                StreetName = requestModel.StreetName,
                Tutor = tutor,
                TutorId = tutor.Id
            };
            tutor.Address = adress;
            var subjects = await _subjectRepository.GetSelectedSubjects(requestModel.SubjectIds);
            foreach (var item in subjects)
            {
                var studentSubjesct = new SubjectTutor
                {
                    Tutor = tutor,
                    TutorId = tutor.Id,
                    Subject = item,
                    SubjectId = item.Id
                };
                tutor.SubjectTutor.Add(studentSubjesct);
            }


            var imageDocuments = new List<DocumentImages>();
                        if(requestModel.DocumentImages.Count != 0)
            {
                foreach(var item in requestModel.DocumentImages)
                {

                    var imgpath = _webpath.WebRootPath;
                    var imagePath = Path.Combine(imgpath, "myImages");
                    Directory.CreateDirectory(imagePath);
                    var imageType = item.ContentType.Split('/')[1];
                    if (imageType == "jpg" || imageType == "png" || imageType == "jpeg")
                    {
                        myImageName = $"{Guid.NewGuid()}.{imageType}";
                        var img = new DocumentImages
                        {
                            ImageName = myImageName,
                            Tutor = tutor,
                            TutorId = tutor.Id
                        };
                      
                        var fullpath = Path.Combine(imagePath, myImageName);
                        using (var filestream = new FileStream(fullpath, FileMode.Create))
                        {
                            item.CopyTo(filestream);
                        }
                        imageDocuments.Add(img);
                    }
                    else
                    {
                        throw new Exception("Unsorported format");
                    }

                }
            }
           
            tutor.DocumentImages = imageDocuments;


            await _userRepository.Create(user);
            var adminss = await _tutorRepository.Create(tutor);
            return new TutorResponseModel
            {
                Message = "Tutor created successfully",
                Status = true,
                Data = new TutorDto
                {
                    Id = tutor.Id,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = tutor.Address.HouseNumber,
                        LGAOfResidence = tutor.Address.LGAOfResidence,
                        State = tutor.Address.State,
                        StreetName = tutor.Address.StreetName,
                    },
                    City = tutor.City,
                    Status = tutor.Status,
                    MailAddress = tutor.MailAddress,
                    DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
                    ProfilePicture = tutor.ProfilePicture,
                    Qualification = tutor.Qualification
                }
            };
        }

        public async Task<BaseResponse> DeleteTutor(int Id)
        {
            var tutor = await _tutorRepository.GetTutor(x => x.Id == Id);
            if (tutor == null)
            {
                return new TutorResponseModel
                {
                    Message = "Tutor not found",
                    Status = false
                };
            }
            tutor.IsDeleted = true;
            await _tutorRepository.Update(tutor);
            return new TutorResponseModel
            {
                Status = true,
                Message = "Tutor Successfully deleted",
              
               
            };
        }
    

    public async Task<TutorsResponseModel> GetAllTutor()
    {
        var tutors = await _tutorRepository.GetAllTutor();
        if (tutors.Count == 0)
        {
            return new TutorsResponseModel
            {
                Status = false,
                Message = "Tutor List is empty"
            };
        }
        return new TutorsResponseModel
        {
            Data = tutors.Select(tutor => new TutorDto
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                Address = new AddressDto
                {
                    HouseNumber = tutor.Address.HouseNumber,
                    LGAOfResidence = tutor.Address.LGAOfResidence,
                    State = tutor.Address.State,
                    StreetName = tutor.Address.StreetName,
                },
                City = tutor.City,
                Status = tutor.Status,
                MailAddress = tutor.MailAddress,
               // DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
                ProfilePicture = tutor.ProfilePicture,
                Qualification = tutor.Qualification
            }).ToList(),
            Status = true,
            Message = "Tutor Successfully Retrieved"
        };
    }

    
    public async Task<TutorResponseModel> GetTutorById(int id)
    {
            var tutor = await _tutorRepository.GetTutor(x => x.UserId == id);
        if (tutor == null)
        {
            return new TutorResponseModel
            {
                Status = false,
                Message = "Tutor Not Found"
            };
        }
            return new TutorResponseModel
            {
                Data = new TutorDto
                {
                    Id = tutor.Id,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = tutor.Address.HouseNumber,
                        LGAOfResidence = tutor.Address.LGAOfResidence,
                        State = tutor.Address.State,
                        StreetName = tutor.Address.StreetName,
                    },
                City = tutor.City,
                Status = tutor.Status,
                MailAddress = tutor.MailAddress,
               DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
               ProfilePicture = tutor.ProfilePicture,
                Qualification = tutor.Qualification
            },
            Status = true,
            Message = "Tutor Successfully Retrieved"
        };
    }
    public async Task<TutorsResponseModel> GetTutorByQualification(Qualification qualification)
    {
        var tutor = await _tutorRepository.GetTutorByQualification(qualification);
        if (tutor.Count == 0)
        {
            return new TutorsResponseModel
            {
                Status = false,
                Message = "Tutor Not Found"
            };
        }
        return new TutorsResponseModel
        {
            Data = tutor.Select(a => new TutorDto
            {
                Id = a.Id,
                FirstName = a.FirstName,
                LastName = a.LastName,
                 Address = new AddressDto
                 {
                     HouseNumber = a.Address.HouseNumber,
                     LGAOfResidence = a.Address.LGAOfResidence,
                     State = a.Address.State,
                     StreetName = a.Address.StreetName,
                 },
                City = a.City,
                Status = a.Status,
                MailAddress = a.MailAddress,
              //  DocumentImages = a.DocumentImages.Select(a => a.ImageName).ToList(),
                ProfilePicture = a.ProfilePicture,
                Qualification = a.Qualification
            }).ToList(),
            Status = true,
            Message = "Tutor Successfully Retrieved"
        };
    }

    public async Task<TutorResponseModel> UpdateTutor(TutorUpdateModel updateModel, int id)
    {
        var tutor = await _tutorRepository.GetTutor(x => x.Id == id);
        if (tutor == null)
        {
            return new TutorResponseModel
            {
                Message = "Tutor not found",
                Status = false
            };
        }

            var myImageName = "";
            var list = new List<SubjectTutor>();
            var imageDocuments = new List<DocumentImages>();
            if (updateModel.ProfilePicture != null)
            {
                var imgpath = _webpath.WebRootPath;
                var imagePath = Path.Combine(imgpath, "myImages");
                Directory.CreateDirectory(imagePath);
                var imageType = updateModel.ProfilePicture.ContentType.Split('/')[1];
                if (imageType == "jpg" || imageType == "png" || imageType == "jpeg")
                {
                    myImageName = $"{Guid.NewGuid()}.{imageType}";
                    var fullpath = Path.Combine(imagePath, myImageName);
                    using (var filestream = new FileStream(fullpath, FileMode.Create))
                    {
                        updateModel.ProfilePicture.CopyTo(filestream);
                    }
                }
                else
                {
                    throw new Exception("Unsorported format");
                }

            }
            if (updateModel.DocumentImages.Count != 0)
            {
                foreach (var item in updateModel.DocumentImages)
                {

                    var imgpath = _webpath.WebRootPath;
                    var imagePath = Path.Combine(imgpath, "myImages");
                    Directory.CreateDirectory(imagePath);
                    var imageType = item.ContentType.Split('/')[1];
                    if (imageType == "jpg" || imageType == "png" || imageType == "jpeg")
                    {
                        var img = new DocumentImages
                        {
                            ImageName = $"{Guid.NewGuid()}.{imageType}",
                            Tutor= tutor,
                            TutorId = tutor.Id
                        };
                        
                        var fullpath = Path.Combine(imagePath, myImageName);
                        using (var filestream = new FileStream(fullpath, FileMode.Create))
                        {
                            item.CopyTo(filestream);
                        }
                        imageDocuments.Add(img);
                    }
                    else
                    {
                        throw new Exception("Unsorported format");
                    }

                }
            }
            tutor.MailAddress = updateModel.MailAddress ?? tutor.MailAddress;
            tutor.FirstName = updateModel.FirstName ?? tutor.FirstName ;
        tutor.LastName = updateModel.LastName ?? tutor.LastName;
        tutor.ProfilePicture = myImageName ?? tutor.ProfilePicture;
        tutor.DocumentImages = (imageDocuments.Count == 0) ? tutor.DocumentImages : imageDocuments;
        tutor.Qualification = (updateModel.Qualification.ToString() == null) ? tutor.Qualification : updateModel.Qualification;
            var subjects = await _subjectRepository.GetSelectedSubjects(updateModel.SubjectIds);
            foreach (var item in subjects)
            {
                var studentSubjesct = new SubjectTutor
                {
                    Tutor = tutor,
                    TutorId = tutor.Id,
                    Subject = item,
                    SubjectId = item.Id
                };
              list.Add(studentSubjesct);
            }
            tutor.SubjectTutor = list;
            tutor.MailAddress = updateModel.MailAddress;
      
       var tutors =  await _tutorRepository.Update(tutor);
        return new TutorResponseModel
        {
            Status = true,
            Message = "Tutor Successfully Updated",
            Data = new TutorDto
            {
                Id = tutor.Id,
                FirstName = tutor.FirstName,
                LastName = tutor.LastName,
                //Address = new AddressDto
                //{
                //    HouseNumber = tutors.Address.HouseNumber,
                //    LGAOfResidence = tutors.Address.LGAOfResidence,
                //    State = tutors.Address.State,
                //    StreetName = tutors.Address.StreetName,
                //},
                City = tutor.City,
                Status = tutor.Status,
                MailAddress = tutor.MailAddress,
               // DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
                ProfilePicture = tutor.ProfilePicture,
                Qualification = tutor.Qualification
            },
        };
    }

        public async Task<TutorsResponseModel> GetTutorsByLocalGovernment(string name)
        {
            var tutors = await _tutorRepository.GetTutorsByLocalGovernment(name);
            if (tutors.Count == 0)
            {
                return new TutorsResponseModel
                {
                    Status = false,
                    Message = "Tutor List is empty"
                };
            }
            return new TutorsResponseModel
            {
                Data = tutors.Select(tutor => new TutorDto
                {
                    Id = tutor.Id,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = tutor.Address.HouseNumber,
                        LGAOfResidence = tutor.Address.LGAOfResidence,
                        State = tutor.Address.State,
                        StreetName = tutor.Address.StreetName,
                    },
                    City = tutor.City,
                    Status = tutor.Status,
                    MailAddress = tutor.MailAddress,
                  //  DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
                    ProfilePicture = tutor.ProfilePicture,
                    Qualification = tutor.Qualification
                }).ToList(),
                Status = true,
                Message = "Tutor Successfully Retrieved"
            };
        }

        public async Task<TutorsResponseModel> GetTutorsByState(string name)
        {
            var tutors = await _tutorRepository.GetTutorsByState(name);
            if (tutors.Count == 0)
            {
                return new TutorsResponseModel
                {
                    Status = false,
                    Message = "Tutor List is empty"
                };
            }
            return new TutorsResponseModel
            {
                Data = tutors.Select(tutor => new TutorDto
                {
                    Id = tutor.Id,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = tutor.Address.HouseNumber,
                        LGAOfResidence = tutor.Address.LGAOfResidence,
                        State = tutor.Address.State,
                        StreetName = tutor.Address.StreetName,
                    },
                    City = tutor.City,
                    Status = tutor.Status,
                    MailAddress = tutor.MailAddress,
                  //  DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
                    ProfilePicture = tutor.ProfilePicture,
                    Qualification = tutor.Qualification
                }).ToList(),
                Status = true,
                Message = "Tutor Successfully Retrieved"
            };
        }

        public async Task<TutorResponseModel> GetTutorByIdForAdmin(int id)
        {
            var tutor = await _tutorRepository.GetTutor(x => x.Id == id);
            if (tutor == null)
            {
                return new TutorResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new TutorResponseModel
            {
                Data = new TutorDto
                {
                    Id = tutor.Id,
                    FirstName = tutor.FirstName,
                    LastName = tutor.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = tutor.Address.HouseNumber,
                        LGAOfResidence = tutor.Address.LGAOfResidence,
                        State = tutor.Address.State,
                        StreetName = tutor.Address.StreetName,
                    },
                    Subjects = tutor.SubjectTutor.Select(a => new SubjectDto
                    {
                        Name = a.Subject.Name,
                        Id = a.SubjectId
                    }).ToList(),
                    
                    City = tutor.City,
                    Status = tutor.Status,
                    MailAddress = tutor.MailAddress,
                    DocumentImages = tutor.DocumentImages.Select(a => a.ImageName).ToList(),
                    ProfilePicture = tutor.ProfilePicture,
                    Qualification = tutor.Qualification
                },
                Status = true,
                Message = "Tutor Successfully Retrieved"
            };
        }

        public IList<string> GetEnumQualification()
        {
            var response = Enum.GetValues(typeof(Qualification)).Cast<int>().ToList();
            List<string> qualification = new List<string>();
            foreach (var item in response)
            {
                qualification.Add(Enum.GetName(typeof(Qualification) , item));
            }

            return qualification;
        }


        public async Task<TutorsResponseModel> GetTutorBySpecialization(Specialization specialization)
        {
            var tutor = await _tutorRepository.GetAllTutorBySpecialization(specialization);
            if (tutor.Count == 0)
            {
                return new TutorsResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new TutorsResponseModel
            {
                Data = tutor.Select(a => new TutorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = a.Address.HouseNumber,
                        LGAOfResidence = a.Address.LGAOfResidence,
                        State = a.Address.State,
                        StreetName = a.Address.StreetName,
                    },
                    City = a.City,
                    Status = a.Status,
                    MailAddress = a.MailAddress,
                    //  DocumentImages = a.DocumentImages.Select(a => a.ImageName).ToList(),
                    ProfilePicture = a.ProfilePicture,
                    Qualification = a.Qualification
                }).ToList(),
                Status = true,
                Message = "Tutor Successfully Retrieved"
            };
        }

        public async Task<TutorsResponseModel> GetTutorByStatus(Status status)
        {
            var tutor = await _tutorRepository.GetAllTutorByStatus(status);
            if (tutor.Count == 0)
            {
                return new TutorsResponseModel
                {
                    Status = false,
                    Message = "Tutor Not Found"
                };
            }
            return new TutorsResponseModel
            {
                Data = tutor.Select(a => new TutorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName,
                    Address = new AddressDto
                    {
                        HouseNumber = a.Address.HouseNumber,
                        LGAOfResidence = a.Address.LGAOfResidence,
                        State = a.Address.State,
                        StreetName = a.Address.StreetName,
                    },
                    City = a.City,
                    Status = a.Status,
                    MailAddress = a.MailAddress,
                    //  DocumentImages = a.DocumentImages.Select(a => a.ImageName).ToList(),
                    ProfilePicture = a.ProfilePicture,
                    Qualification = a.Qualification
                }).ToList(),
                Status = true,
                Message = "Tutor Successfully Retrieved"
            };
        }

        public IList<string> GetEnumBySpecialization()
        {
            var response = Enum.GetValues(typeof(Specialization)).Cast<int>().ToList();
            List<string> specializationType = new List<string>();
            foreach (var item in response)
            {
                specializationType.Add(Enum.GetName(typeof(Specialization), item));
            }

            return specializationType;
        }

        public IList<string> GetEnumByStatus()
        {
            var response = Enum.GetValues(typeof(Status)).Cast<int>().ToList();
            List<string> statusType = new List<string>();
            foreach (var item in response)
            {
                statusType.Add(Enum.GetName(typeof(Status), item));
            }

            return statusType;
        }

        public async Task<TutorsResponseModel> GetTutorsAccordingToStudent(int id)
        {
            var list = new List<TutorDto>();
            var list2 = new List<Tutor>();
            var student = await _studentRepository.GetStudent(x => x.Id == id);
            if(student.Subjects.Count == 0)
            {
                return new TutorsResponseModel
                {
                    Status = false,
                    Message = "No subject attached to this student"

                };
            }

            foreach(var item in student.Subjects)
            {
                var tutors = await _tutorRepository.GetAllTutorDependingOnSubject(x => x.SubjectTutor.Any(a => a.SubjectId == item.SubjectId) && x.Address.State == student.Address.State);

                foreach (var tutor in tutors)
                {
                    
                    if(!list2.Contains(tutor))
                    {
                        list2.Add(tutor);
                        list.Add(new TutorDto
                        {
                            Id = tutor.Id,
                            FirstName = tutor.FirstName,
                            LastName = tutor.LastName,
                            Address = new AddressDto
                            {
                                HouseNumber = tutor.Address.HouseNumber,
                                LGAOfResidence = tutor.Address.LGAOfResidence,
                                State = tutor.Address.State,
                                StreetName = tutor.Address.StreetName,
                            },
                            City = tutor.City,
                            Status = tutor.Status,
                            MailAddress = tutor.MailAddress,
                            //  DocumentImages = a.DocumentImages.Select(a => a.ImageName).ToList(),
                            ProfilePicture = tutor.ProfilePicture,
                            Qualification = tutor.Qualification
                        });
                    }
                  
                }
            }

            return new TutorsResponseModel
            {
                Status = true,
                Message = "Tutor sucessfully retrieved",
                Data = list
            };
        }
    }
}

