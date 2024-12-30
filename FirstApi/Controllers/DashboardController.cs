using FirstApi.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DashboardController:ControllerBase
    {
        private readonly AppDbContext _context;

       
        public DashboardController(AppDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Dashboard Details 
        /// </summary>
        // GET: api/Dashboard
        [HttpGet]
        public async Task<IActionResult> Dashboard()
        {
            var projectDetails = await _context.Projects
                           .Select(p => new
                           {
                               p.Id,
                               p.Name,
                               p.Description,
                               p.Status,
                               StatusTitle = p.Status,
                               p.Priority,
                               p.OwnerId,
                               p.Budget,
                               p.ProgressPercentage
                           })
                           .ToListAsync();
            int totalPercentage = 0;
            foreach (var project in projectDetails)
            {
                switch (project.Status)
                {
                    case "Planned":
                        // Assuming 0% completion for "Planned"
                        break;
                    case "In Progress":
                        // Assuming 50% completion for "In Progress"
                        totalPercentage += 50;
                        break;
                    case "Completed":
                        // Assuming 100% completion for "Completed"
                        totalPercentage += 100;
                        break;
                    default:
                        // Optional: Handle unexpected statuses if necessary
                        break;
                }
            }

            double averageCompletion = projectDetails.Count > 0
                ? (double)totalPercentage / projectDetails.Count
                : 0;

            // Fetch User Details
            var userDetails = await _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.Name,
                    u.Email,
                    u.PhoneNumber,
                    u.TeamId,
                    TeamName = u.Team != null ? u.Team.TeamName : null
                })
                .ToListAsync();

            // Fetch Team Details
            var teamDetails = await _context.Teams
                .Select(t => new
                {
                    t.Id,
                    t.TeamName,
                    t.Description,
                    MemberCount = t.Members.Count,
                    Members = t.Members.Select(m => new { m.Id, m.Name }).ToList()
                })
                .ToListAsync();
            var queryDetails = await _context.Queries
               .Select(t => new
               {
                   t.Id,
                   t.Name,
                   t.Description,
    
               })
               .ToListAsync();


            // Combine Data into a Single Object
            var dashboardData = new
            {
                AverageProjectCompletion = averageCompletion,
                Projects = projectDetails,
                Users = userDetails,
                Teams = teamDetails,
                Queries = queryDetails
            };

            // Return the Combined Data
            return Ok(dashboardData);
        }
    }

}

