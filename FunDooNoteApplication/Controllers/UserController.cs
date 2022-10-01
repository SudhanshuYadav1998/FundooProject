using BusinessLayer.Interface;
using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            catch (System.Exception)
            {

                throw;
            }
        }

    }
}
