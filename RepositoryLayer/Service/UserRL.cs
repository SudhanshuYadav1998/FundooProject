using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly fundoocontext _fundooContext;
        IConfiguration _Appsettings;
        public UserRL(fundoocontext fundooContext, IConfiguration Appsettings)
        {
            _fundooContext = fundooContext;
            _Appsettings = Appsettings;
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
       
        public string UserLogin(string Email, string Password)
        {
            try
            {
                var Enteredlogin = this._fundooContext.UserTable.Where(X => X.Email == Email && X.Password == Password).FirstOrDefault();
                if (Enteredlogin != null)

                {
                    string token = GenerateSecurityToken(Enteredlogin.Email, Enteredlogin.UserId);
                    return token;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GenerateSecurityToken(string Email, long UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Appsettings["Jwt:SecKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] {
                new Claim(ClaimTypes.Email,Email),
                new Claim("UserId",UserId.ToString())
            };
            var token = new JwtSecurityToken(_Appsettings["Jwt:Issuer"],
              _Appsettings["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(60),
              signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public string ForgetPassword(string Email)
        {
            try
            {
                var EmailCheck = _fundooContext.UserTable.FirstOrDefault(e => e.Email == Email);
                if(EmailCheck !=null)
                {
                    var token=GenerateSecurityToken(EmailCheck.Email,EmailCheck.UserId);
                    MSMQModel mSMQModel=new MSMQModel();
                    mSMQModel.sendData2Queue(token);
                    return token.ToString();
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool ResetPassword(string email, string password, string confirmPassword)
        {
            try
            {
                if (password.Equals(confirmPassword))
                {
                    UserEntity user = _fundooContext.UserTable.Where(e => e.Email == email).FirstOrDefault();
                    user.Password = confirmPassword;
                    _fundooContext.SaveChanges();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
