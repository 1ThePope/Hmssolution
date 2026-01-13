using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hmssolution.DBContext;
using Hmssolution.HmsDTOs.PendingUserDTOs;
using Hmssolution.HmsDTOs.AuthDTOs;
using Hmssolution.HmsModel;
using Hmssolution.Services.JwtService;

namespace Hmssolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HmssolutionContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtService _jwt;

        public AuthController(HmssolutionContext context, IMapper mapper, UserManager<IdentityUser> userManager, JwtService jwt)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _jwt = jwt;
        }

        // REGISTER (NO OTP)
        [HttpPost("register")]
        public async Task<IActionResult> Register(PendingUserCreate dto)
        {
            //make sure email format is inputted
            if(!dto.EmailAddress.Contains("@") || !dto.EmailAddress.EndsWith(".com", StringComparison.OrdinalIgnoreCase))
            return BadRequest("Wrong Email Input");

            // Check if email already exists
            if (await _userManager.FindByEmailAsync(dto.EmailAddress) != null)
                return BadRequest("Email already used");

            // Create Identity user
            var user = new IdentityUser
            {
                UserName = dto.EmailAddress,
                Email = dto.EmailAddress,
                EmailConfirmed = true
            };

            var createRes = await _userManager.CreateAsync(user, dto.Password);
            if (!createRes.Succeeded)
                return BadRequest(createRes.Errors.First().Description);

            // Assign role
            await _userManager.AddToRoleAsync(user, dto.Role);

            // Add domain-specific records
            if (dto.Role == "Student")
            {
                var student = new Student
                {
                    FullName = dto?.FullName,
                    EmailAddress = dto.EmailAddress,
                    PhoneNumber = dto.PhoneNumber ?? string.Empty,
                    Gender = dto.Gender ?? string.Empty,
                    Faculty = dto.Faculty ?? string.Empty,
                    Department = dto.Department ?? string.Empty,
                    MatricNo = dto.MatricNo ?? string.Empty,
                    Level = dto.Level,
                    Religion = dto.Religion ?? string.Empty,
                    Photo = Array.Empty<byte>()
                };

                _context.Students.Add(student);
            }
            else if (dto.Role == "LandLord")
            {
                var landlord = new LandLord
                {
                    FullName = dto?.FullName,
                    EmailAddress = dto.EmailAddress,
                    PhoneNumber = dto.PhoneNumber ?? string.Empty,
                    Gender = dto.Gender ?? string.Empty,
                    Verification = Array.Empty<byte>()
                };

                _context.LandLords.Add(landlord);
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = "Account created successfully" });
        }


        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest dto)
        {
            var user = await _userManager.FindByEmailAsync(dto?.Email);

            if(!dto.Email.Contains("@") && dto.Email.EndsWith(".com", StringComparison.OrdinalIgnoreCase))
            return BadRequest("Wrong Email Input");

            if (user == null)
                return Unauthorized("Invalid credentials");

            var valid = await _userManager.CheckPasswordAsync(user, dto?.Password);
            if (!valid)
                return Unauthorized("Invalid credentials");

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? "Student";

            string userIdForToken = user.Id; // default for Student

            // 🔥 If LandLord, get the LandLordID from your LandLord table
            if (role == "LandLord")
            {
                var landlord = await _context.LandLords
                    .FirstOrDefaultAsync(l => l.EmailAddress == dto.Email);

                if (landlord == null)
                    return Unauthorized("Landlord account not found");

                userIdForToken = landlord.LandLordID.ToString(); // <<-- IMPORTANT
            }

            // 🔥 If Student, get the StudentID from your Students table
            else if (role == "Student")
            {
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.EmailAddress == dto.Email);

                if (student != null)
                    userIdForToken = student.StudentID.ToString();
            }

             var token = _jwt.GenerateToken(userIdForToken, user?.Email, role);

            return Ok(new { token, role });
        }
    }
}
