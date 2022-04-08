using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OvertimeApi.DataAceess;
using System.Linq;
using System.Threading.Tasks;

namespace OvertimeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class OvertimeController : ControllerBase
    {


        public OvertimeController(Context context)
        {
            _context = context;
        }
        private Context _context { get; set; }




        [HttpGet("all")]
        public async Task<IActionResult> GetALL()
        {
            var values = await _context.Overtimes.OrderBy(x=>x.DateTime).ToListAsync();
            return Ok(values);
        }
        [HttpPost("create")]
        public async Task<IActionResult> Add(Overtime p)
        {
            await _context.Overtimes.AddAsync(p);
            await _context.SaveChangesAsync();
            return Ok();

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var val = await _context.Overtimes.FindAsync(id);
            if (val == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(val);
            }
        }


        [HttpPut("edit")]
        public async Task<IActionResult> Update(Overtime p)

        {
            var value = await _context.FindAsync<Overtime>(p.İD);
            if (value == null)
            {
                return NotFound();
            }
            else
            {
                value.DateTime = p.DateTime;
                value.Description = p.Description;
                _context.SaveChanges(); 
                await _context.SaveChangesAsync();
                return Ok();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var value = await _context.FindAsync<Overtime>(id);
            if (value == null)
            {
                return NotFound();

            }
            else
            {
                _context.Remove(value);
                await _context.SaveChangesAsync();
                return Ok();
            }

        }
        [HttpGet("days")]
        public async Task<IActionResult> Getdays()
        {
            var value = await _context.Overtimes.CountAsync();
            string message = "Your overtime days is: " + value;
            return Ok(message);
        }



    }
}
