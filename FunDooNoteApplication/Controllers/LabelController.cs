using BusinessLayer.Service;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Entities;
using RepositoryLayer.Service;
using System.Linq;
using System;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Interface;
using RepositoryLayer.Context;
using System.Collections.Generic;

namespace FunDooNoteApplication.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly Fundoocontext fundoocontext;
        public LabelController(ILabelBL labelBL,Fundoocontext fundoocontext)
        {
            this.labelBL = labelBL;
            this.fundoocontext = fundoocontext;
        }  

       
        [HttpPost("Create")]
        public IActionResult CreateLabel(LabelModel labelModel)
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.labelBL.CreateLabel(labelModel);
                if (result != null && userid != 0)
                {
                    return this.Ok(new { success = true, message = "Label Added Successfully",data=labelModel });

                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong" });

                }
            }
            catch (SystemException)
            {

                throw;
            }
        }
       
        
        [HttpGet("GetAllLabel")]
        public List<LabelEntity> GetLabel()
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.labelBL.GetAllLabel(userid);
                return result;
            }
            catch (SystemException)
            {

                throw;
            }
        }
        [HttpDelete("DeleteLabel")]
        public IActionResult DeleteLabel(long labelid)
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.labelBL.DeleteLabel(labelid);
                if (result != false && userid != 0)
                {
                    return this.Ok(new { success = true, message = "Label Added Successfully" });

                }
                else
                {
                    return this.BadRequest(new { success = false, message = "Something Goes Wrong" });

                }
            }
            catch (SystemException)
            {

                throw;
            }
        }
    }
}
