using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace FunDooNoteApplication.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }
        [HttpPost("Register")]
        public IActionResult UserRegistration(UserRegistration userRegistration)
        {
            try
            {
                var result = userBL.Registration(userRegistration);
                if(result != null)
                {
                    return this.Ok(new {success=true,message="Registration Successfull",data=result});
                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Registration UnSuccessfull"});

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("AllLogin")]
        public IActionResult UserLogin(string Email, string Password)
        {
            try
            {
                var result = userBL.UserLogin( Email, Password);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong,Login Unsuccessful" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [HttpPost("ForgotPassword")]
        public IActionResult ForgetPassword(string Email)
        {
            try
            {
                var result = userBL.ForgetPassword(Email);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Mail Sent Succesfully"});
                }
                else
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(string password, string confirmPassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var result = userBL.ResetPassword(email,password,confirmPassword);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Password Changed Succesfully" });
                }
                else
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
