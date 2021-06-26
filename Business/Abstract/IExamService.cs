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
        Exam GetById(int id);
        List<Exam> GetAll();
        void Create(Exam entity);
        void Update(Exam entity);
        void Delete(Exam entity);
    }
}
