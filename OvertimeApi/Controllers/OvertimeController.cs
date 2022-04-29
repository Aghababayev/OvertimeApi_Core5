using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OvertimeApi.DataAceess;
using OvertimeApi.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OvertimeController : ControllerBase
    {

        
        public OvertimeController(Context context, ILogger<OvertimeController> logger)
        {
            _context = context;  //_______________Data Access Entity (Dependency Injection) 
            _logger = logger;  //___Logging process standart
        }
        private Context _context { get; set; }
        private ILogger _logger { get; set; }
       
        [HttpGet("all")]
      //[Authorize(Roles ="User,Admin")]
        public async Task<IActionResult> GetALL()
        {
     
           
            try
            {
                var values = await _context.Overtimes.OrderBy(x => x.DateTime).ToListAsync();

                _logger.LogInformation("All data has been imported succesfully ");
                return Ok(values);
            }
            catch (Exception exc)
            {

                _logger.LogError(exc, "OOOOPPPPPPS");
                return NotFound();
            }

        }
        [HttpPost("create")]
        //[Authorize(Roles = "Admin")]
      
        public async Task<IActionResult> Add(OvertimePostVM overtimeVM)
        {
            var overtime = new Overtime()
            {
                DateTime = overtimeVM.DateTime,
                Description = overtimeVM.Description,
            };
            _logger.LogInformation("Data has been importded succesfully ");
            await _context.Overtimes.AddAsync(overtime);
            await _context.SaveChangesAsync();

            return Ok();

        }
        [HttpGet("get/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(int id)
        {
            var val = await _context.Overtimes.FindAsync(id);
            if (val == null)
            {
                _logger.LogError("Id is unreachable");
                return NotFound();
            }
            else
            {
                _logger.LogInformation("ID {@id}  founded succesfully", id);
                return Ok(val);
            }
        }


        [HttpPut("edit")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Overtime p)

        {
            var value = await _context.FindAsync<Overtime>(p.İD);
            if (value == null)
            {
                _logger.LogError("Id {@p.ID} isnt't exist", p.İD);
                return NotFound();
            }
            else
            {
                value.DateTime = p.DateTime;
                value.Description = p.Description;
                await _context.SaveChangesAsync();
                _logger.LogWarning("Row has been updated succesfully: \r\nId={@p.ID}  \r\nDescription={@p.Description}  \r\nDate={@p.DateTime}", p.İD, p.Description, p.DateTime);
                return Ok();
            }
        }
        [HttpDelete("delete/{id}")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _context.FindAsync<Overtime>(id);
            if (value == null)
            {
                _logger.LogError("Id {@id} isnt't exist", id);
                return NotFound();

            }
            else
            {

                _context.Remove(value);
                await _context.SaveChangesAsync();
                _logger.LogWarning("Row has been removed succesfully: Id={@id}", id);
                return Ok();
            }

        }
        [HttpGet("days")]
        //[Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Getdays()
        {
            var value = await _context.Overtimes.CountAsync();
            string message = "Your overtime days: " + value;
            _logger.LogInformation(message);
            return Ok(message);
        }



    }
}
