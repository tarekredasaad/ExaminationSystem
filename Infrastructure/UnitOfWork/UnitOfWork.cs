﻿using Domain;
using Domain.Interfaces.Repository;
using Infrastructure.Repository;
using Domain.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly Context Context;
        public IRepository<User> UserRepository { get;private set; }

       

        //public IRepository<assessment_answers> assessment_AnswersRepo { get; private set; }
        //public IRepository<assessment_questions> assessment_QuestionsRepo { get; private set; }
        //public IRepository<assessment_questions_relation> assessment_Questions_RelationRepo{ get; private set; }

        //public IRepository<Assessments> assessmentRepo { get; private set; }
        //public IRepository<assessment_options> assessment_optionsRepo { get; private set; }
        //public IRepository<assessment_meta> assessment_metaRepo { get; private set; }
        //public IRepository<assessment_match> assessment_matchRepo{ get; private set; }

        //public IRepository<assessment_enrols> assessment_enrolsRepo { get; private set; }
        //public IRepository<assessment_data> assessment_dataRepo { get; private set; }
        //public IRepository<assessment_department> assessment_departmentRepo { get; private set; }
        //public IRepository<assessment_sections> assessment_sectionsRepo { get; private set; }

        //public IRepository<assessment_text> assessment_textRepo { get; private set; }

        //public IRepository<assessment_true_false> assessment_true_falseRepo { get; private set; }

        public IRepository<Instructor> InstructorRepo { get; private set; }
        public IRepository<Student> StudentRepo { get; private set; }
        public IRepository<CourseStudent> CourseStudentRepo { get; private set; }
        public IRepository<Course> CourseRepo { get; private set; }
        public IRepository<Answer> AnswerRepo { get; private set; }
        public IRepository<Exam> ExamRepo { get; private set; }
        public IRepository<ExamQuestion> ExamQuestionRepo { get; private set; }
        public IRepository<StudentExam> StudentExamRepo { get; private set; }
        public IRepository<Question> QuestionRepo { get; private set; }
        public IRepository<Choice> ChoiceRepo { get; private set; }
        public IRepository<Tasks> TasksRepo { get; private set; }

        public IRepository<TeamMember> TeamMemberRepo { get; private set; }
        //IDbContextTransaction transaction;
        public UnitOfWork(Context context) 
        {
            Context = context;
            //transaction = Context.Database.BeginTransaction();
            UserRepository = new Repository<User>(Context);
            InstructorRepo = new Repository<Instructor>(Context);
            StudentRepo = new Repository<Student>(Context);
            CourseStudentRepo = new Repository<CourseStudent>(Context);
            CourseRepo = new Repository<Course>(Context);
            ExamRepo = new Repository<Exam>(Context);
            ExamQuestionRepo = new Repository<ExamQuestion>(Context);
            StudentExamRepo = new Repository<StudentExam>(Context);
            QuestionRepo = new Repository<Question>(Context);
            AnswerRepo = new Repository<Answer>(Context);
            ChoiceRepo = new Repository<Choice>(Context);
            
             TasksRepo = new Repository<Tasks>(Context);
            TeamMemberRepo = new Repository<TeamMember>(Context);
        }
        public async Task commit() 
        {
            try
            {
                await Context.SaveChangesAsync();
                //Context.Database.CurrentTransaction.Commit();
            }
            catch(Exception ex) 
            {
                Context.Database.CurrentTransaction.Rollback();
                //Console.WriteLine(ex.InnerException.ToString() + ex.StackTrace.ToString() );
                throw;
            }
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}
