using FirstApi.Data;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriorityController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PriorityController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves all priorities.
        /// </summary>
        /// <returns>A list of priorities.</returns>
        /// <response code="200">Returns the list of priorities.</response>
        /// <response code="404">No priorities found.</response>
        // GET: api/<PriorityController>
        [HttpGet]
        public async Task<IActionResult> GetPriority()
        {
            var prioroty = await _context.Priorities.ToListAsync();
            if (prioroty.Count == 0)
            {
                return NotFound("No prioroty found.");
            }
            return Ok(prioroty);
        }

        // GET api/<PriorityController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriorityById(int id)
        {
            var priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound($"priority with ID {id} not found.");
            }
            return Ok(priority);
        }

        // POST api/<PriorityController>
        [HttpPost]
        public async Task<IActionResult> CreatePriority([FromBody] Priority priority)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid priority data.");
            }

            await _context.Priorities.AddAsync(priority);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "priority created successfully.",
                IssueId = priority.Id,
                IssueName = priority.Title,
            });
        }

        // PUT api/<PriorityController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePriority(int id, [FromBody] Priority priority)
        {
            if (id != priority.Id)
            {
                return BadRequest("priority ID mismatch.");
            }

            var existingpriority = await _context.Priorities.FindAsync(id);
            if (existingpriority == null)
            {
                return NotFound($"priority with ID {id} not found.");
            }
            existingpriority.Title = priority.Title;


            _context.Entry(existingpriority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the priority.");
            }

            return Ok($"priority with ID {id} updated successfully.");
        }

        // DELETE api/<PriorityController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(int id)
        {
            var priority = await _context.Priorities.FindAsync(id);
            if (priority == null)
            {
                return NotFound($"priority with ID {id} not found.");
            }

            _context.Priorities.Remove(priority);
            await _context.SaveChangesAsync();

            return Ok($"priority with ID {id} deleted successfully.");
        }
    }
}
