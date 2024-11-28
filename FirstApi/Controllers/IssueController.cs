using FirstApi.Data;
using FirstApi.Models; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssueController : ControllerBase
    {
        private readonly AppDbContext _context;

        public IssueController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<IssueController>
        [HttpGet]
        public async Task<IActionResult> GetIssues()
        {
            var issues = await _context.Issues.Include(i => i.Project).ToListAsync();
            if (issues.Count == 0)
            {
                return NotFound("No Issue found.");
            }
            return Ok(issues);
        }

        // GET api/<IssueController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetIssueById(int id)
        {
            var issue = await _context.Issues
               .Include(i => i.Project)
               .FirstOrDefaultAsync(i => i.Id == id);
            if (issue == null)
            {
                return NotFound($"Issue with ID {id} not found.");
            }
            return Ok(issue);
        }

        // POST api/<IssueController>
        [HttpPost]
        public  async Task<IActionResult> CreateIssue([FromBody] Issue issue)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid issue data.");

            // Ensure the Project exists
            var project = await _context.Projects.FindAsync(issue.ProjectId);
            if (project == null)
                return BadRequest("Invalid ProjectId. The referenced project does not exist.");

            issue.CreatedDate = DateTime.Now;
            issue.LastUpdatedDate = DateTime.Now;

            _context.Issues.Add(issue);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetIssueById), new { id = issue.Id }, issue);
        }


        // PUT api/<IssueController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIssue(int id, [FromBody] Issue issue)
        {
            if (id != issue.Id)
            {
                return BadRequest("Issue ID mismatch.");
            }

            var existingIssue = await _context.Issues.FindAsync(id);
            if (existingIssue == null)
            {
                return NotFound($"Issue with ID {id} not found.");
            }

            // Update the Issue details
            existingIssue.Title = issue.Title;
            existingIssue.Description = issue.Description;
            existingIssue.ProjectId = issue.ProjectId;
            existingIssue.Status = issue.Status;
            existingIssue.Priority = issue.Priority;
            existingIssue.StartDate = issue.StartDate;
            existingIssue.DueDate = issue.DueDate;
            existingIssue.CompletedDate = issue.CompletedDate; // Nullable
            existingIssue.LastUpdatedDate = DateTime.Now; // Automatically set the last updated time
            existingIssue.AssignedTo = issue.AssignedTo ?? existingIssue.AssignedTo; // Update only if provided
            existingIssue.Tags = issue.Tags ?? existingIssue.Tags; // Update only if provided
            existingIssue.EstimatedHours = issue.EstimatedHours;
            existingIssue.CreatedBy = issue.CreatedBy;


            _context.Entry(existingIssue).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the Issue.");
            }

            return Ok($"Issue with ID {id} updated successfully.");
        }

        // DELETE api/<IssueController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIssue(int id)
        {
            var issue = await _context.Issues.FindAsync(id);
            if (issue == null)
            {
                return NotFound($"Issue with ID {id} not found.");
            }

            _context.Issues.Remove(issue);
            await _context.SaveChangesAsync();

            return Ok($"Issues with ID {id} deleted successfully.");
        }
    }
}
