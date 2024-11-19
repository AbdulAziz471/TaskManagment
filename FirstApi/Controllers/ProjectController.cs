using FirstApi.Data;
using FirstApi.Modals; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProjectController(AppDbContext context)
        {
            _context = context;
        }
        // GET: api/<ProjectController>
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            var projects = await _context.Projects.ToListAsync();
            if (projects.Count == 0)
            {
                return NotFound("No projects found.");
            }
            return Ok(projects);
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }
            return Ok(project);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid project data.");
            }

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Project created successfully.",
                ProjectId = project.Id,
                ProjectName = project.Name,
                CreatedAt = project.CreatedDate
            });
        }

        // PUT api/<ProjectController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProject(int id, [FromBody] Project project)
        {
            if (id != project.Id)
            {
                return BadRequest("Project ID mismatch.");
            }

            var existingProject = await _context.Projects.FindAsync(id);
            if (existingProject == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            // Update the project details
            existingProject.Name = project.Name;
            existingProject.Description = project.Description;
            existingProject.Status = project.Status;
            existingProject.Priority = project.Priority;
            existingProject.StartDate = project.StartDate;
            existingProject.EndDate = project.EndDate;
            existingProject.LastUpdatedDate = DateTime.Now; // Update the last updated date
            existingProject.OwnerId = project.OwnerId;
            existingProject.Budget = project.Budget;
            existingProject.Cost = project.Cost;
            existingProject.Category = project.Category;
            existingProject.ClientId = project.ClientId;
            existingProject.ProgressPercentage = project.ProgressPercentage;
            existingProject.IsArchived = project.IsArchived;

            // Update list fields
            existingProject.AssignedTo = project.AssignedTo ?? existingProject.AssignedTo;
            existingProject.Tags = project.Tags ?? existingProject.Tags;
            existingProject.Documents = project.Documents ?? existingProject.Documents;

            _context.Entry(existingProject).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the project.");
            }

            return Ok($"Project with ID {id} updated successfully.");
        }


        // DELETE api/<ProjectController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var project = await _context.Projects.FindAsync(id);
            if (project == null)
            {
                return NotFound($"Project with ID {id} not found.");
            }

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return Ok($"Project with ID {id} deleted successfully.");
        }
    }
}
