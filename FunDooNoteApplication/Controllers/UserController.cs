﻿using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult UserLogin(UserLoginModel userLogin)
        {
            try
            {
                var result = userBL.UserLogin(userLogin);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Login Successful", data = result });
                }
                else
                    return this.BadRequest(new { success = false, message = "Login Unsuccessful" });
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
                    return this.BadRequest(new { success = false, message = "Unsuccessful" });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }

}
