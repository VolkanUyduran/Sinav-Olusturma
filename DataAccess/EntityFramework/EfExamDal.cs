using DataAccess.Abstract;
using DataAccess.Concrete.Repositories;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.EntityFramework
{
   public  class EfExamDal:GenericRepository<Exam>,IExamDal
    {

    }
}
