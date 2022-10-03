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
        private readonly FundooContext _fundooContext;
        IConfiguration _Appsettings;
        public UserRL(FundooContext fundooContext, IConfiguration Appsettings)
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
        public LoginResponseModel UserLogin(UserLoginModel info)
        {
            try
            {
                var Enteredlogin = this._fundooContext.UserTable.Where(X => X.Email == info.Email && X.Password==info.Password).FirstOrDefault();
                if ((Enteredlogin.Password) == info.Password)

                {
                    LoginResponseModel data = new LoginResponseModel();
                    string token = GenerateSecurityToken(Enteredlogin.Email, Enteredlogin.UserId);
                    data.Id = Enteredlogin.UserId;
                    data.FirstName = Enteredlogin.FirstName;
                    data.LastName = Enteredlogin.LastName;
                    data.Email = Enteredlogin.Email;
                    data.Password = Enteredlogin.Password;
                    data.Token = token;
                    return data;
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
    }
}
