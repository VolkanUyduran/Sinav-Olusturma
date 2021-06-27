using Entities.Concrete;
using Entities.Dto.Exam;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IExamService
    {
        TitleDto GetDataFromWebSite();
        ExamDto GetById(int id);
        List<Exam> GetAll();
        void Create(Exam entity);
        void Update(Exam entity);
        Exam Delete(int id);
        List<string> CompletedExam(int examId, string[] correctAnswers);
    }
}
