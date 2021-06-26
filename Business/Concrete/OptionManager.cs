using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OptionManager:IOptionService
    {
        IOptionDal _optionDal;

        public OptionManager(IOptionDal optionDal)
        {
            _optionDal = optionDal;
        }

        public void Create(Option entity)
        {
            _optionDal.Insert(entity);
        }

        public void Delete(Option entity)
        {
            _optionDal.Delete(entity);
        }

        public List<Option> GetAll()
        {
            return _optionDal.List();
        }

        public Option GetById(int id)
        {
            return _optionDal.Get(x => x.OptionId == id);
        }

        public void Update(Option entity)
        {
            _optionDal.Update(entity);
        }
    }
}
