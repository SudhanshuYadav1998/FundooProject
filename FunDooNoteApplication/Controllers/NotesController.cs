using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using BusinessLayer.Interface;
using RepositoryLayer.Context;
using RepositoryLayer.Entities;
using System.Collections.Generic;

namespace FunDooNoteApplication.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly Fundoocontext fundooContext;
        public NotesController(INoteBL noteBL,Fundoocontext fundoocontext)
        {
            this.fundooContext = fundoocontext;
            this.noteBL = noteBL;
        }

        [HttpPost("Create")]
        public IActionResult GenerateNote(NotesModel noteModel)
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.noteBL.GenerateNote(noteModel, userId);

                if (result!=null)
                {
                    return this.Ok(new { success = true, message = "Note Added Successfully",data=noteModel });
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
        [HttpGet("GetAllNotes")]
        public List<Note> GetAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result =  this.noteBL.GetAllNotes(userId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
