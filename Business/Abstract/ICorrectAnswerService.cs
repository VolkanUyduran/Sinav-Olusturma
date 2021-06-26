using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface ICorrectAnswerService
    {
        UserAnswer GetById(int id);
        List<UserAnswer> GetAll();
        void Create(UserAnswer entity);
        void Update(UserAnswer entity);
        void Delete(UserAnswer entity);
    }
}
