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
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog;

namespace FunDooNoteApplication.Controllers
{
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class LabelController : ControllerBase
    {
        private readonly ILabelBL labelBL;
        private readonly Fundoocontext fundoocontext;
        private readonly IMemoryCache memoryCache;
        private readonly IDistributedCache distributedCache;
        private readonly ILogger<LabelController> _logger;
        public LabelController(ILabelBL labelBL,Fundoocontext fundoocontext, IMemoryCache memoryCache, IDistributedCache distributedCache, ILogger<LabelController> _logger)
        {
            this.labelBL = labelBL;
            this.fundoocontext = fundoocontext;
            this.memoryCache = memoryCache;
            this.distributedCache = distributedCache;
            this._logger = _logger;
        }  

       
        [HttpPost("Create")]
        public IActionResult CreateLabel(LabelModel labelModel)
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                _logger.LogInformation("User id matched create Label");

                var result = this.labelBL.CreateLabel(labelModel);
                
                if (result != null && userid != 0)
                {
                    _logger.LogInformation("User id and result matched in create Label");

                    return this.Ok(new { success = true, message = "Label Added Successfully", data=labelModel });

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
                _logger.LogInformation("User id matched in get all Label");
                return result;
            }
            catch (SystemException)
            {

                throw;
            }
        }
       
        [HttpDelete("Delete")]
        public IActionResult DeleteLabel(long labelid)
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                _logger.LogInformation("User id matched in Delete Label");

                var result = this.labelBL.DeleteLabel(labelid);
                if (result != false && userid != 0)
                {
                    _logger.LogInformation("User id and result matched in Delete Label");

                    return this.Ok(new { success = true, message = "Label deleted Successfully" });

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
        [HttpPut("Update")]
        public IActionResult UpdateLabel(LabelModel labelModel,long labelid)
        {
            try
            {
                var userid = Convert.ToInt32(User.Claims.FirstOrDefault(e => e.Type == "UserId").Value);
                
                var result = this.labelBL.UpdateLabel(labelModel,labelid);
                _logger.LogInformation("User id matched in Update Label");
                if (result != false && userid != 0)
                {
                    _logger.LogInformation("User id and result matched in Update Label");
                    return this.Ok(new { success = true, message = "Label Updated Successfully" });

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
        [HttpGet("Redis")]
        public async Task<IActionResult> GetAllLabelUsingRedisCache()
        {
            var cacheKey = "LabelList";
            string serializedLabelList;
            var LabelList = new List<LabelEntity>();
            var redisLabelList = await distributedCache.GetAsync(cacheKey);
            if (redisLabelList != null)
            {
                serializedLabelList = Encoding.UTF8.GetString(redisLabelList);
                LabelList = JsonConvert.DeserializeObject<List<LabelEntity>>(serializedLabelList);
            }
            else
            {
                LabelList = await fundoocontext.LabelTable.ToListAsync();
                serializedLabelList = JsonConvert.SerializeObject(LabelList);
                redisLabelList = Encoding.UTF8.GetBytes(serializedLabelList);
                var options = new DistributedCacheEntryOptions()
                .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisLabelList, options);
            }
            return Ok(LabelList);
        }
    }
}
