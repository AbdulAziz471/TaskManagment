using FirstApi.Data;
using FirstApi.DTO;
using FirstApi.Models; 
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
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            // Fetch all projects and include related issues
            var projects = await _context.Projects
                .Include(p => p.Issues) // Include the Issues navigation property
                .ToListAsync();

            // Check if any projects exist
            if (!projects.Any())
                return NotFound(new { Message = "No projects found." });

            // Return the list of projects
            return Ok(projects);
        }

        // GET api/<ProjectController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProjectById(int id)
        {
            var project = await _context.Projects
       .Include(p => p.Issues) // Includes the Issues relationship
       .FirstOrDefaultAsync(p => p.Id == id);

            // Return 404 if project is not found
            if (project == null)
                return NotFound(new { Message = $"Project with ID {id} not found." });

            // Return the project directly
            return Ok(project);
        }

        // POST api/<ProjectController>
        [HttpPost]
        public async Task<IActionResult> CreateProject([FromBody] ProjectDTO projectDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    Message = "Invalid project data.",
                    Errors = ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage))
                });
            }

            // Map the DTO to the Project entity
            var project = new Project
            {
                Name = projectDto.Name,
                Description = projectDto.Description,
                Status = projectDto.Status,
                Priority = projectDto.Priority,
                StartDate = projectDto.StartDate,
                EndDate = projectDto.EndDate,
                OwnerId = projectDto.OwnerId,
                AssignedTo = projectDto.AssignedTo,
                Budget = projectDto.Budget,
                Cost = projectDto.Cost,
                Category = projectDto.Category,
                Tags = projectDto.Tags,
                ClientId = projectDto.ClientId,
                ProgressPercentage = projectDto.ProgressPercentage,
                IsArchived = projectDto.IsArchived,
                Documents = projectDto.Documents,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now
            };

            // Save the project to the database
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProjectById), new { id = project.Id }, new
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
