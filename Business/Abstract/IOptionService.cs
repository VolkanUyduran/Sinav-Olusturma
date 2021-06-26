using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IOptionService
    {
        Option GetById(int id);
        List<Option> GetAll();
        void Create(Option entity);
        void Update(Option entity);
        void Delete(Option entity);
    }
}
