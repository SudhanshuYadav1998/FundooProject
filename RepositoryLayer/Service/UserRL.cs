using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL:IUserRL
    {
        private readonly Fundoocontext _fundooContext;
        IConfiguration _Appsettings;
        private readonly ILogger<UserRL> _logger;
        public UserRL(Fundoocontext fundooContext, IConfiguration Appsettings,ILogger<UserRL> logger)
        {
            _fundooContext = fundooContext;
            _Appsettings = Appsettings;
            _logger=logger;
        }
        public UserEntity Registration(UserRegistration userRegistration)
        {
            try
            {
                UserEntity userEntity = new UserEntity
                {
                    FirstName = userRegistration.FirstName,
                    LastName = userRegistration.LastName,
                    Email = userRegistration.Email,
                    Password = Encrypt(userRegistration.Password),
                };
                _logger.LogInformation("User Added SuccessFully");
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
       
        public string UserLogin(UserLogin userlogin)
        {
            try
            {
                var Enteredlogin = this._fundooContext.UserTable.Where(X => X.Email == userlogin.Email).FirstOrDefault();
                if (Decrypt(Enteredlogin.Password)==userlogin.Password)

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
        public string Encrypt(string encryptString)
        {
            string EncryptionKey = "Hide";
            byte[] clearBytes = Encoding.Unicode.GetBytes(encryptString);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
               
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptString = Convert.ToBase64String(ms.ToArray());
                }
            }
            return encryptString;
        }

        public string Decrypt(string cipherText)
        {
            string EncryptionKey = "Hide";
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
            0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76
        });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
