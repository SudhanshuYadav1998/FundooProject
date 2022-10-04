using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL:IUserBL
    {
        private readonly IUserRL userRl;
        public UserBL(IUserRL userRl)
        {
            this.userRl = userRl;
        }

        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
            {
                return userRl.Registration(userRegistration);
            }
            catch (Exception ex) 
            {

                throw ex;
            }
        }
        LoginResponseModel IUserBL.UserLogin(UserLoginModel user)
        {
            try
            {
                return userRl.UserLogin(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string ForgetPassword(string Email)
        {
            try
            {
                return userRl.ForgetPassword(Email);

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
