using CommonLayer.Model;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly FundooContext _fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            _fundooContext = fundooContext;
        }
        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity();
                userEntity.FirstName = userRegistration.FirstName;
                userEntity.LastName = userRegistration.LastName;
                userEntity.Email = userRegistration.Email;
                userEntity.Password = userRegistration.Password;
                _fundooContext.UserTable.Add(userEntity);
                int result=_fundooContext.SaveChanges();
                if(result > 0)
                {
                    return userEntity;
                }
                else { return null; }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
    }
}
