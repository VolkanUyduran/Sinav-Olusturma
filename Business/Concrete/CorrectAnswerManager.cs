using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CorrectAnswerManager : ICorrectAnswerService
    {
        ICorrectAnswerDal _correctAnswerDal;

        public CorrectAnswerManager(ICorrectAnswerDal correctAnswerDal)
        {
            _correctAnswerDal = correctAnswerDal;
        }

        public void Create(UserAnswer entity)
        {
            _correctAnswerDal.Insert(entity);
        }

        public void Delete(UserAnswer entity)
        {
            _correctAnswerDal.Delete(entity);
        }

        public List<UserAnswer> GetAll()
        {
            return _correctAnswerDal.List();
        }

        public UserAnswer GetById(int id)
        {
            return _correctAnswerDal.Get(x => x.OptionId == id);
        }

        public void Update(UserAnswer entity)
        {
            _correctAnswerDal.Update(entity);
        }
    }
}
