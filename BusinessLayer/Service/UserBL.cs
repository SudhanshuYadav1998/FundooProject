using BusinessLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
