using System.Text;
using AutoMapper;
using Hmssolution.DBContext;
using Hmssolution.HmsDTOs;
using Hmssolution.HmsDTOs.HostelDTOs;
using Hmssolution.HmsModel;
using MailKit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Hmssolution.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq; 

namespace Hmssolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly HmssolutionContext _Context;
        private readonly IMapper _Mapper;
        public StudentController(HmssolutionContext context, IMapper mapper)
        {
            _Context = context;
            _Mapper = mapper;
        }

        //Student profile by id
        [Authorize(Roles= "Student,Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentRead>> GetStudent(int id)
        {
            var email = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.Email)?.Value;
            if (email == null) return Unauthorized();

           // var role = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;            

            // Get logged-in student's ID from claim
            //var studentIDClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
           // if (studentIDClaim == null) return Unauthorized();

           // int loggedInStudentID = int.Parse(studentIDClaim);

            // If role is Student, they can only view their own profile
           // if (role == "Student" && loggedInStudentId != id)
            //return Forbid("You are not allowed to view other students' profiles");

            var Student = await _Context.Students
            .Include(s => s.Booking).ThenInclude(b => b.Hostel)
            .Include(s => s.Booking).ThenInclude(b => b.Room)
            .FirstOrDefaultAsync(s=>s.StudentID== id);

            if (Student == null) return NotFound();
            return _Mapper.Map<StudentRead>(Student);
        }
        //Student profile
        [Authorize(Roles= "Admin")]
        [HttpGet("profile")]
        public async Task<ActionResult<List<StudentRead>>> StudentProfile()
        {
            var StudentID = User.Claims.FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (StudentID == null) return Unauthorized();

           // int StudentID=int.Parse(studentIDClaim);

            var Student= await _Context.Students
            .Include(s => s.Booking).ThenInclude(b => b.Hostel)
            .Include(s => s.Booking).ThenInclude(b => b.Room)
            .ToListAsync();
            
            if (!Student.Any())
            return NotFound("Student Profile not available");
            return _Mapper.Map<List<StudentRead>>(Student);
        }
       
    }
}
