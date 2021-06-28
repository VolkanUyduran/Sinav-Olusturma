using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dto.User;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public User Create(UserDto model)
        {
            var user = model.Adapt<User>();

            _userDal.Insert(user);
            return user;
        }

        public void Delete(User entity)
        {
            _userDal.Delete(entity);
        }

        public List<User> GetAll()
        {
            return _userDal.List();
        }

        public User GetById(int id)
        {
            return _userDal.Get(x => x.UserId == id);
        }

        public void Update(User entity)
        {
            _userDal.Update(entity);
        }

        public User UserLogin(UserDto model) {

            var user = _userDal.Get(x => x.UserName == model.UserName && x.Password == model.Password);
            return user;
        
        }
    }
}