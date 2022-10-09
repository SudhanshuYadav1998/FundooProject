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
        public NotesController(INoteBL noteBL, Fundoocontext fundoocontext)
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

                if (result != null)
                {
                    return this.Ok(new { success = true, message = "Note Added Successfully", data = noteModel });
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
        public List<NotesEntity> GetAllNotes()
        {
            try
            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var result = this.noteBL.GetAllNotes(userId);
                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [HttpDelete("Delete")]
        public IActionResult DeleteNotesOfUser(long notesid)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                if (this.noteBL.DeleteApi(notesid))
                {
                    return this.Ok(new { Success = true, message = "Deleted successfully" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Registration Found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("Update")]
        public IActionResult UpdateNotesOfUser(NotesModel notesModel,long noteId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                NotesModel notes = this.noteBL.UpdateNote(notesModel,noteId);
                if (notes!=null )
                {
                    return this.Ok(new { Success = true, message = "Note updated successfully",data=notesModel });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Note Found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("Pin")]
        public IActionResult Pinned( long noteId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var notes = this.noteBL.Pinned(noteId);
                if (notes != false)
                {
                    return this.Ok(new { Success = true, message = "You have pinned your Note" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Note Found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("Trash")]
        public IActionResult Trash(long noteId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var notes = this.noteBL.Trash( noteId);
                if (notes != false)
                {
                    return this.Ok(new { Success = true, message = "You have pinned your Note" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Note Found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPut("Archieve")]
        public IActionResult Archieve(long noteId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var notes = this.noteBL.Archieve(noteId);
                if (notes != false)
                {
                    return this.Ok(new { Success = true, message = "You have pinned your Note" });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "No Such Note Found" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
