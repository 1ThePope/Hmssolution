using AutoMapper;
using Hmssolution.DBContext;
using Hmssolution.HmsDTOs.LandLordDTOs;
using Hmssolution.HmsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Hmssolution.HmsDTOs.HostelDTOs;
using System.Security.Claims;

namespace Hmssolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LandLordController : ControllerBase
    {
        private readonly HmssolutionContext _Context;
        private readonly IMapper _Mapper;
        public LandLordController(HmssolutionContext context,IMapper mapper)
        {
            _Context=context;
            _Mapper=mapper;
        }
        //LandLord Profile
        [Authorize(Roles= "Admin")]
        [HttpGet("Profile")]
        public async Task<ActionResult<List<LandLordRead>>> GetLandLord()
        {
            var LandLord= await _Context.LandLords.Include(s=>s.Hostels).ToListAsync();
            if(!LandLord.Any())
            return NotFound("Profile not Available");
            return _Mapper.Map<List<LandLordRead>>(LandLord);
        }
        //LandLord by id
        [Authorize(Roles="Admin,LandLord")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var landlordIdClaim= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(landlordIdClaim != id.ToString() && !User.IsInRole("Admin"))
            {
                return Forbid("You are not authorized to view this landlord's profile.");
            }
            var landlord = await _Context.LandLords.Include(l => l.Hostels)
            .FirstOrDefaultAsync(l => l.LandLordID == id);
            if (landlord == null) return NotFound();
            return Ok(_Mapper.Map<LandLordRead>(landlord));
        }

        [Authorize(Roles = "LandLord")]
        [HttpGet("myhostels")]
        public async Task<ActionResult<List<HostelRead>>> GetMyHostels()
        {
            // 1. Get the logged-in landlord ID from the JWT
            var landlordIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (landlordIdClaim == null )
                return Unauthorized("You are not authorized to view hostels.");

            // 2. Fetch only hostels that belong to this landlord
            var hostels = await _Context.Hostels
                .Where(h => h.LandLordID.ToString() == landlordIdClaim)
                .Include(h => h.Rooms) // optional if you want rooms included
                .ToListAsync();

            // 3. Return 404 if landlord has no hostels
            if (!hostels.Any())
                return NotFound("You currently have no hostels.");

            // 4. Map to DTO and return
            return Ok(_Mapper.Map<List<HostelRead>>(hostels));
        }


        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var landlord = await _Context.LandLords.FindAsync(id);
            if (landlord == null) return NotFound();
            _Context.LandLords.Remove(landlord);
            await _Context.SaveChangesAsync();
            return NoContent();
        }
    }
}
