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
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FunDooNoteApplication.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]

    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private readonly INoteBL noteBL;
        private readonly Fundoocontext fundooContext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        public NotesController(INoteBL noteBL, Fundoocontext fundoocontext, IMemoryCache memoryCache, IDistributedCache distributedCache)
        {
            this.fundooContext = fundoocontext;
            this.noteBL = noteBL;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
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
        public IActionResult UpdateNotesOfUser(NotesModel notesModel, long noteId)
        {
            try

            {
                long userId = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                var notes = this.noteBL.UpdateNote(notesModel, noteId);
                if (notes != false)
                {
                    return this.Ok(new { Success = true, message = "Note updated successfully", data = notesModel });
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
        public IActionResult Pinned(long noteId)
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
                var notes = this.noteBL.Trash(noteId);
                if (notes != false)
                {
                    return this.Ok(new { Success = true, message = "your Note is in trash" });
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
                    return this.Ok(new { Success = true, message = "your Note is archieved " });
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
        [HttpPut("Image")]
        public IActionResult BGImage(long NotesId, IFormFile image)
        {
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "UserId").Value);
                    var result = this.noteBL.BGImage(NotesId, image);
                    if (result == true)
                    {
                        return this.Ok(new { isSuccess = true, message = "BGImage Added Successfully!", data = result });
                    }
                    else
                        return this.BadRequest(new { isSuccess = false, message = " BGImage not Added!" });
                }
                catch (Exception ex)
                {
                    return this.BadRequest(new { Status = false, isSuccess = false, message = ex.InnerException.Message });
                }
            }


        }
        [HttpPut("AddColor")]
        public IActionResult AddColor(long NotesId, string color)
        {
            {
                try
                {
                    long userId = Convert.ToInt32(User.Claims.FirstOrDefault(X => X.Type == "UserId").Value);
                    var result = this.noteBL.AddColor(NotesId, color);
                    if (result == true)
                    {
                        return this.Ok(new { isSuccess = true, message = "Color Added Successfully!", data = result });
                    }
                    else
                        return this.BadRequest(new { isSuccess = false, message = " Color not Added!" });
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        [HttpGet("Redis")]
             async Task<IActionResult> GetAllNotesUsingRedisCache()
            {
                var cacheKey = "NotesList";
                string serializedNotesList;
                var NotesList = new List<NotesEntity>();
                var redisNotesList = await distributedCache.GetAsync(cacheKey);
                if (redisNotesList != null)
                {
                    serializedNotesList = Encoding.UTF8.GetString(redisNotesList);
                    NotesList = JsonConvert.DeserializeObject<List<NotesEntity>>(serializedNotesList);
                }
                else
                {
                    NotesList = await fundooContext.NotesTable.ToListAsync();
                    serializedNotesList = JsonConvert.SerializeObject(NotesList);
                    redisNotesList = Encoding.UTF8.GetBytes(serializedNotesList);
                    var options = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                        .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                    await distributedCache.SetAsync(cacheKey, redisNotesList, options);
                }
                return Ok(NotesList);
            }
        }
    }

