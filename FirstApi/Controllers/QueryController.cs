using FirstApi.Data;
using FirstApi.DTO.Query;
using FirstApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class QueryController : ControllerBase
    {

        private readonly AppDbContext _context;
        public QueryController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all Queries.
        /// </summary>
        // GET: api/<QueryController>
        [HttpGet]
        public  async Task<IActionResult> GetQueries()
        {
            var query = await _context.Queries.
                 Include(q => q.Status)
                .Include(q => q.Priority)
                .Include(q => q.CreatedBy)
                .Include(q => q.Project)
                .Include(q => q.AssignedUser)
               .Select(q => new QueryListDto
               {
                   Id = q.Id,
                   Name = q.Name,
                   Description = q.Description,
                   StatusName = q.Status.Title,  
                   PriorityName = q.Priority.Title,  
                   StartDate = q.StartDate,
                   EndDate = q.EndDate,
                   CreatedDate = q.CreatedDate,
                   LastUpdatedDate = q.LastUpdatedDate,
                   CreatedByName = q.CreatedBy.Name,  
                   AssignedUserName = q.AssignedUser.Name,  
                   ProjectName = q.Project.Name  
               })
        .ToListAsync();
            if (query.Count == 0)
            {
                return NotFound("No Query found.");
            }
            return Ok(query);
        }


        /// <summary>
        /// Retrieve A Query By Its ID.
        /// </summary>
        // GET api/<QueryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQueryById(int id)
        {
            var query = await _context.Queries
                .Include(q => q.Status)
                .Include(q => q.Priority)
                .Include(q => q.CreatedBy)
                .Include(q => q.Project)
                .Include(q => q.AssignedUser)
                .Select(q => new QueryListDto
                {
                    Id = q.Id,
                    Name = q.Name,
                    Description = q.Description,
                    StatusName = q.Status.Title, 
                    PriorityName = q.Priority.Title, 
                    StartDate = q.StartDate,
                    EndDate = q.EndDate,
                    CreatedDate = q.CreatedDate,
                    LastUpdatedDate = q.LastUpdatedDate,
                    CreatedByName = q.CreatedBy.Name, 
                    AssignedUserName = q.AssignedUser.Name, 
                    ProjectName = q.Project.Name 
                })
                .FirstOrDefaultAsync(q => q.Id == id);

            if (query == null)
            {
                return NotFound($"Query with ID {id} not found.");
            }
            return Ok(query);
        }



        /// <summary>
        /// Create A Query
        /// </summary>
        // POST api/<QueryController>
        [HttpPost]
        public async Task<IActionResult> CreateQuery([FromBody] CreateQueryDto createQueryDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate Priority ID
            bool priorityExists = await _context.Priorities.AnyAsync(p => p.Id == createQueryDto.PriorityId);
            if (!priorityExists)
            {
                return BadRequest("Invalid Priority ID.");
            }

            // Validate Status ID
            bool statusExists = await _context.Statuses.AnyAsync(s => s.Id == createQueryDto.StatusId);
            if (!statusExists)
            {
                return BadRequest("Invalid Status ID.");
            }

            // Validate Project ID
            bool projectExists = await _context.Projects.AnyAsync(p => p.Id == createQueryDto.ProjectId);
            if (!projectExists)
            {
                return BadRequest("Invalid Project ID.");
            }

            // Validate CreatedBy ID
            bool userExists = await _context.Users.AnyAsync(u => u.Id == createQueryDto.CreatedById);
            if (!userExists)
            {
                return BadRequest("Invalid User ID for Created By.");
            }

            
            Query newQuery = new Query
            {
                // Populate properties from DTO
                Name = createQueryDto.Name,
                Description = createQueryDto.Description,
                StatusId = createQueryDto.StatusId,
                PriorityId = createQueryDto.PriorityId,
                StartDate = createQueryDto.StartDate,
                EndDate = createQueryDto.EndDate,
                ProjectId = createQueryDto.ProjectId,
                CreatedById = createQueryDto.CreatedById,
                AssignedUserId = createQueryDto.AssignedUserId,
                CreatedDate = DateTime.Now,
                LastUpdatedDate = DateTime.Now
            };

            _context.Queries.Add(newQuery);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Query created successfully.",
                QueryId = newQuery.Id,
                QueryName = newQuery.Name
            });
        }



        /// <summary>
        /// Update  A Query
        /// </summary>
        // PUT api/<QueryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateQuery(int id, [FromBody] CreateQueryDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Invalid request data.");
            }

            var existingQuery = await _context.Queries.FindAsync(id);
            if (existingQuery == null)
            {
                return NotFound($"query with ID {id} not found.");
            }
            existingQuery.Name = updateDto.Name;
            existingQuery.Description = updateDto.Description;
            existingQuery.StatusId = updateDto.StatusId;
            existingQuery.PriorityId = updateDto.PriorityId;
            existingQuery.StartDate = updateDto.StartDate;
            existingQuery.EndDate = updateDto.EndDate;
            existingQuery.ProjectId = updateDto.ProjectId;
            existingQuery.AssignedUserId = updateDto.AssignedUserId;

            existingQuery.LastUpdatedDate = DateTime.Now; 


            try
            {
                await _context.SaveChangesAsync();
                return Ok($"Query with ID {id} updated successfully.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return StatusCode(500, "An error occurred while updating the query. " + ex.Message);
            }
        }


        /// <summary>
        /// Delete A Query By Its ID
        /// </summary>
        // DELETE api/<QueryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuery(int id)
        {
            var query = await _context.Queries.FindAsync(id);
            if (query == null)
            {
                return NotFound($"query with ID {id} not found.");
            }

            _context.Queries.Remove(query);
            await _context.SaveChangesAsync();

            return Ok($"query with ID {id} deleted successfully.");
        }
    }
}
