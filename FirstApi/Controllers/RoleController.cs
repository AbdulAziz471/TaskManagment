using FirstApi.Data;
using FirstApi.DTO;
using FirstApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly AppDbContext _context;
        public RoleController(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Get All Roles
        /// </summary>
        // GET: api/<RoleController>
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _context.Roles.ToListAsync();
            if (roles.Count == 0)
            {
                return NotFound("No Status found.");
            }
            return Ok(roles);
        }


        /// <summary>
        /// Get A Role By Its ID 
        /// </summary>
        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }
            return Ok(role);
        }



        /// <summary>
        /// Create A Role 
        /// </summary>

        // POST api/<RoleController>
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid role data.");
            }
            var role = new Role
            {
                Title = roleDto.Title
            };
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "role created successfully.",
                RoleId = role.Id,
                RoleName = role.Title,
            });
        }


        /// <summary>
        /// Udpate  A Role By Its ID.
        /// </summary>
        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] Role role)
        {
            if (id != role.Id)
            {
                return BadRequest("role ID mismatch.");
            }

            var existingRole = await _context.Roles.FindAsync(id);
            if (existingRole == null)
            {
                return NotFound($"status with ID {id} not found.");
            }
            existingRole.Title = role.Title;


            _context.Entry(existingRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(500, "An error occurred while updating the role.");
            }

            return Ok($"role with ID {id} updated successfully.");
        }



        /// <summary>
        /// Delete  A Role By Its ID.
        /// </summary>
        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound($"role with ID {id} not found.");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok($"role with ID {id} deleted successfully.");
        }
    }
}
