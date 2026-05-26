using FoodSlot.Models;
using FoodSlot.Services.GoogleMonitoringService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodSlot.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIRequestLogsController : ControllerBase
    {
        private readonly FoodSlotContext _context;
        private readonly GoogleMonitoringService _googleMonitoringService;

        public APIRequestLogsController(FoodSlotContext context, GoogleMonitoringService googleMonitoringService)
        {
            _context = context;
            _googleMonitoringService= googleMonitoringService;
        }

        // GET: api/APIRequestLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<APIRequestLog>>> GetAll()
        {
            return await _context.APIRequestLog.ToListAsync();
        }

        // GET: api/APIRequestLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<APIRequestLog>> Get(long id)
        {
            var log = await _context.APIRequestLog.FindAsync(id);
            if (log == null) return NotFound();
            return log;
        }

        // POST: api/APIRequestLogs/fetch
        // 從 Google Monitoring 抓資料並存入資料庫
        [HttpPost("fetch")]
        public async Task<IActionResult> FetchFromGoogle()
        {
            await _googleMonitoringService.FetchAndSaveAsync(24);
            return Ok("資料已從 Google Monitoring 更新");
        }

        // DELETE: api/APIRequestLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var log = await _context.APIRequestLog.FindAsync(id);
            if (log == null) return NotFound();
            _context.APIRequestLog.Remove(log);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
