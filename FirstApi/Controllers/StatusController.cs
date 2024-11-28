using FirstApi.Data;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StatusController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<StatusController>
        [HttpGet]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _context.Statuses.ToListAsync();
            if (status.Count == 0)
            {
                return NotFound("No Status found.");
            }
            return Ok(status);
        }

        // GET api/<StatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStatusById(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound($"Status with ID {id} not found.");
            }
            return Ok(status);
        }

        // POST api/<StatusController>
        [HttpPost]
        public async Task<IActionResult> CreateStatus([FromBody] Status status)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Status data.");
            }

            await _context.Statuses.AddAsync(status);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "status created successfully.",
                IssueId = status.Id,
                IssueName = status.Title,
            });
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] Status status)
        {
            if (id != status.Id)
            {
                return BadRequest("status ID mismatch.");
            }

            var existingStatus = await _context.Statuses.FindAsync(id);
            if (existingStatus == null)
            {
                return NotFound($"status with ID {id} not found.");
            }
            existingStatus.Title = status.Title;


            _context.Entry(existingStatus).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the Status.");
            }

            return Ok($"Status with ID {id} updated successfully.");
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound($"status with ID {id} not found.");
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return Ok($"status with ID {id} deleted successfully.");
        }
    }
}
