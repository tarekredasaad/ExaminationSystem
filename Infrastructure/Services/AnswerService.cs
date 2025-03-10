﻿using AutoMapper;
using Domain.DTO;
using Domain.Interfaces.Services;
using Domain.Interfaces.UnitOfWork;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AnswerService :IAnswerService
    {
        private readonly IUnitOfWork _unitOfWork;
        IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AnswerService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResultDTO> SubmitExam(AnswersDTO answerDTO)
        {
            if (answerDTO != null)
            {
                string userName = _httpContextAccessor.HttpContext.Session.GetString("Username");
                Student student = _unitOfWork.StudentRepo.Get(x => x.UserId == int.Parse(userName));
                StudentExam studentExam = _unitOfWork.StudentExamRepo.Get(se=>se.ExamId == answerDTO.ExamId && se.StudentId == student.id);
                    List<Answer> answers = new List<Answer>();
                if(studentExam != null && studentExam.IsTaken)
                {
                    foreach(var _answer in answerDTO.answerQuestions)
                    {
                        Answer answer = new Answer();
                        answer = _mapper.Map<Answer>(_answer);
                        answer.ExamId = answerDTO.ExamId;
                        answer.StudentId = student.id;
                        //answer = await _unitOfWork.AnswerRepo.Create(answer);
                        //_unitOfWork.commit();
                        answers.Add(answer);
                    }
                  answers=  await _unitOfWork.AnswerRepo.AddRange(answers);
                    studentExam.Completed = true;
                    await _unitOfWork.StudentExamRepo.Update(studentExam);
                }
                var result = ResultDTO.Sucess(answers);

                return result; //new ResultDTO() { StatusCode = 200, Data = "You havn't assigned in this exam yet",Message = "You havn't assigned in this exam yet" };
            }
            else
            {
                return new ResultDTO() { StatusCode = 400, Data = "Invalid operation" };
            }
        }

        
    }
}
