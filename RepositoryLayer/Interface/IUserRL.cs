using CommonLayer.Model;
using RepositoryLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        UserEntity Registration(UserRegistration userRegistration);
        LoginResponseModel UserLogin(UserLoginModel info);
        public string ForgetPassword(string Email);

    }
}
