using FirstApi.Data;
using FirstApi.DTO.Team;
using FirstApi.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly AppDbContext _context;
        public TeamController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retrieves all registered Teams.
        /// </summary>
        // GET: api/<TeamController>
        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _context.Teams
       .Include(t => t.Members)
       .ThenInclude(m => m.Roles) // Include Roles for Members
       .Include(t => t.Members)
       .ThenInclude(m => m.Projects) // Include Projects for Members
       .Select(t => new TeamDto
       {
           TeamName = t.TeamName,       // Map TeamName
           Description = t.Description, // Map Description
           Members = t.Members.Select(m => new UserTeam
           {
               UserId = m.Id,              // Map User ID
               Name = m.Name,              // Map Name
               Email = m.Email,            // Map Email
               Roles = m.Roles.Select(r => new RoleTeam
               {
                   RoleId = r.Id,          // Map Role ID
                   RoleName = r.Title       // Map Role Name
               }).ToList(),
               Projects = m.Projects.Select(p => new ProjectTeam
               {
                   ProjectId = p.Id,       // Map Project ID
                   ProjectName = p.Name    // Map Project Name
               }).ToList()
           }).ToList()
       })
       .ToListAsync();

            if (teams == null || teams.Count == 0)
            {
                return NotFound("No teams found.");
            }

            return Ok(teams); // Return the list of TeamDto objects
        }


        /// <summary>
        /// Retrieve a Single Team by Its ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamsById(int id)
        {
            var team = await _context.Teams.Include(t => t.Members).FirstOrDefaultAsync(t => t.Id == id);
            if (team == null)
            {
                return NotFound($"team with ID {id} not found.");
            }
            var result = new
            {
                team.Id,
                team.TeamName,
                team.Description,
                Members = team.Members.Select(m => new { m.Id, m.Name }) // Return only the required member details
            };
            return Ok(result);
        }
        /// <summary>
        /// Create a Single Team 
        /// </summary>
        // POST api/<TeamController>
        [HttpPost]
        //public async Task<IActionResult> CreateTeam([FromBody] TeamDto teamDto)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest("Invalid team data.");
        //    }

        //    var users = await _context.Users
        //        .Where(u=> teamDto.UserIds.Contains(u.Id))
        //        .ToListAsync();
        //    if (!users.Any())
        //    {
        //        return BadRequest("No valid users found for the provided IDs.");
        //    }
        //    var team = new Team
        //    {
        //        TeamName = teamDto.TeamName,
        //        Description = teamDto.Description,
        //        Members = users // Associate the selected users with the team
        //    };

        //    await _context.Teams.AddAsync(team);
        //    await _context.SaveChangesAsync();

        //    return Ok(new
        //    {
        //        Message = "Team created successfully.",
        //        TeamId = team.Id,
        //        TeamName = team.TeamName,
        //        Members = team.Members.Select(u => new { u.Id, u.Name }) // Return only the IDs and names of the members
        //    });
        //}

        /// <summary>
        /// Update a Single Team 
        /// </summary>
        // PUT api/<TeamController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Updateteam (int id, [FromBody] Team team)
        {
            if (id != team.Id)
            {
                return BadRequest("team ID mismatch.");
            }

            var existingteam = await _context.Teams.FindAsync(id);
            if (existingteam == null)
            {
                return NotFound($"team with ID {id} not found.");
            }
            existingteam.TeamName = team.TeamName;


            _context.Entry(existingteam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the team.");
            }

            return Ok($"team with ID {id} updated successfully.");
        }

        /// <summary>
        /// Delete a Single Team 
        /// </summary>
        // DELETE api/<TeamController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var team = await _context.Teams.Include(t => t.Members) // Eagerly load the Members navigation property
        .FirstOrDefaultAsync(t => t.Id == id); ;
            if (team == null)
            {
                return NotFound($"team with ID {id} not found.");
            }

            _context.Teams.Remove(team);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Team with ID {id} deleted successfully.",
                TeamName = team.TeamName
            });
        }
    }
}
