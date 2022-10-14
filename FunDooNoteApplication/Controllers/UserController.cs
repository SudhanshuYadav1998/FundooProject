using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;

namespace FunDooNoteApplication.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL userBL;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserBL userBL, ILogger<UserController> logger)
        {
            this.userBL = userBL;
            _logger = logger;
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
        [HttpPost("Login")]
        public IActionResult UserLogin(UserLogin userlogin)
        {
            try
            {
                var result = userBL.UserLogin(userlogin);

                
                if (result != null)
                {
                    _logger.LogInformation("User exists in Db");
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                    _logger.LogInformation("User does not exists in Db");
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
                if (result != false)
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
