using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICorrectAnswerService
    {
        CorrectAnswer GetById(int id);
        List<CorrectAnswer> GetAll();
        void Create(CorrectAnswer entity);
        void Update(CorrectAnswer entity);
        void Delete(CorrectAnswer entity);
    }
}
