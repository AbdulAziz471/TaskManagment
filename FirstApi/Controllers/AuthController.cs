using FirstApi.Data;
using FirstApi.Modals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Registers a new user with the specified Role.
    /// </summary>
    // Register endpoint
    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        // Check if the email is already registered
        if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
        {
            return BadRequest("Email is already registered.");
        }

        // Fetch roles based on provided RoleIds
        var roles = await _context.Roles
            .Where(r => registerDto.RoleIds.Contains(r.Id))
            .ToListAsync();

        if (!roles.Any())
        {
            return BadRequest("Invalid roles provided.");
        }

        // Generate password hash and salt
        using var hmac = new HMACSHA512();
        var user = new User
        {
            Name = registerDto.Name,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber,
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalt = hmac.Key,
            Roles = roles // Associate roles with the user
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("User registered successfully.");
    }
    /// <summary>
    /// Login A User
    /// </summary>
    // Login endpoint
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginDto loginDto)
    {
        var user = await _context.Users
            .Include(u => u.Roles) // Include roles during login
            .SingleOrDefaultAsync(u => u.Email == loginDto.Email);

        if (user == null)
        {
            return Unauthorized("Invalid email.");
        }

        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i])
                return Unauthorized("Invalid password.");
        }

        // Generate JWT token
        var token = GenerateJwtToken(user);
        return Ok(token);
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes("a_very_long_secure_secret_key_that_is_at_least_64_characters_long"); // Replace "your_secret_key" with a strong secret key

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email)
        };

        // Add roles to claims
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Title)));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddDays(1), // Set token expiration to 1 day
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}
