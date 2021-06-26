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

        public void Create(CorrectAnswer entity)
        {
            _correctAnswerDal.Insert(entity);
        }

        public void Delete(CorrectAnswer entity)
        {
            _correctAnswerDal.Delete(entity);
        }

        public List<CorrectAnswer> GetAll()
        {
            return _correctAnswerDal.List();
        }

        public CorrectAnswer GetById(int id)
        {
            return _correctAnswerDal.Get(x => x.CorrectId == id);
        }

        public void Update(CorrectAnswer entity)
        {
            _correctAnswerDal.Update(entity);
        }
    }
}
