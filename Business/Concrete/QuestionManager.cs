using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class QuestionManager:IQuestionService
    {
        IQuestionDal _questionDal;

        public QuestionManager(IQuestionDal questionDal)
        {
            _questionDal = questionDal;
        }

        public void Create(Question entity)
        {

            _questionDal.Insert(entity);
        }

        public void Delete(Question entity)
        {
            _questionDal.Delete(entity);
        }
        public List<Question> GetAll()
        {
           return _questionDal.List();
        }
        public Question GetById(int id)
        {
           return _questionDal.Get(x => x.QuestionId == id);
        }
        public void Update(Question entity)
        {
            _questionDal.Update(entity);
        }
    }
}
