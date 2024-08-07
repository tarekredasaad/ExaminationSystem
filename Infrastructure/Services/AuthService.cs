using AutoMapper;
using Domain.DTO;
using Domain.Helper;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultDTO> RegisterAsyncInstructor(UserDTO dto)
        {
            User user = new User();
            dto.password = HashPassword.Encrypt( dto.password);
            user = _mapper.Map<User>(dto);
            user = await _unitOfWork.UserRepository.Create(user);

            if(dto.GroupId == 1)
            {
                Instructor instructor  = new Instructor();
                instructor.User = user;
                instructor = await _unitOfWork.InstructorRepo.Create(instructor);
            }
            else
            {
                Student student = new Student();
                student.User = user;
                student = await _unitOfWork.StudentRepo.Create(student);
            }
            //_unitOfWork.commit();
            ResultDTO Result = new ResultDTO()
            {
                StatusCode = 200,
                Data = user,
                Message = "You added the user successfully"
            };
            return ResultDTO.Sucess(user);

            //await _userRepository.AddAsync(user);
        }

        public async Task<ResultDTO> LoginAsyncInstructor(UserLoginDTO dto)
        {
            User instructor = _unitOfWork.UserRepository.Get(i => i.username == dto.username);
                var password = HashPassword.Decrypt( instructor.password);
            if (instructor == null || password != dto.password)
            {
                return (new ResultDTO() { StatusCode = 400, Data = "invalid username or password" , Message = "invalid username or password" });
            }
            
            //// For simplicity, we are not using tokens here. Instead, set a session or a cookie.
            _httpContextAccessor.HttpContext.Session.SetString("Username", instructor.id.ToString());
            return (new ResultDTO() { StatusCode = 200, Data = instructor, Message = "you login successfully" });
        }

       //public async Task<ResultDTO> RegisterAsyncStudent(StudentDTO dto)
       // {
       //     Student student = new Student();
       //     dto.password = HashPassword.Encrypt(dto.password);
       //     student = _mapper.Map<Student>(dto);
       //   student = await  _unitOfWork.StudentRepo.Create(student);
       //     _unitOfWork.commit();
       //     ResultDTO Result = new ResultDTO()
       //     {
       //         StatusCode = 200,
       //         Data = student,
       //         Message = "You added the Instructor successfully"
       //     };
       //     return Result;

       // }

       // public async Task<ResultDTO> LoginAsyncStudent(StudentDTO dto)
       // {
       //     Student student = _unitOfWork.StudentRepo.Get(i => i.username == dto.username);
       //     var password = HashPassword.Decrypt(student.password);
       //     if (student == null || password != dto.password)
       //     {
       //         return (new ResultDTO() { StatusCode = 400, Data = "invalid username or password", Message = "invalid username or password" });
       //     }

       //     //// For simplicity, we are not using tokens here. Instead, set a session or a cookie.
       //     _httpContextAccessor.HttpContext.Session.SetString("Username", student.username);
       //     return (new ResultDTO() { StatusCode = 200, Data = student, Message = "you login successfully" });
       // }
    }
}
