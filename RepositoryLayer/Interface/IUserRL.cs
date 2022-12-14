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
        public string UserLogin(UserLogin userlogin);
        public string ForgetPassword(string Email);
        public bool ResetPassword(string email,string password, string confirmPassword);

    }
}
