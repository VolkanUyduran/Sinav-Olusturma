using Entities.Concrete;
using Entities.Dto.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        User GetById(int id);
        List<User> GetAll();
        User Create(UserDto model);
        void Update(User entity);
        void Delete(User entity);
        User UserLogin(UserDto model);
    }
}
