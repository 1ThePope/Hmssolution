using AutoMapper;
using Hmssolution.DBContext;
using Hmssolution.HmsDTOs.HostelDTOs;
using Hmssolution.HmsModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Security.Claims;


namespace Hmssolution.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly HmssolutionContext _Context;
        private readonly IMapper _Mapper;
        public HostelController (HmssolutionContext context,IMapper mapper)
        {
            _Context=context;
            _Mapper=mapper;
        }
        //Getting List of Hostels
        [Authorize(Roles="Admin,Student")]
        [HttpGet("Profile")]
        public async Task<ActionResult<List<HostelRead>>> GetHostel()
        {
           var Hostel= await _Context.Hostels
           .Include(r=>r.Rooms)
           .ToListAsync();
           if(!Hostel.Any()) return NotFound("Hostels are currently not Available");
           return _Mapper.Map<List<HostelRead>>(Hostel);
        }

        [Authorize(Roles="Admin,LandLord,Student")]
        [HttpGet("{id}")] public async Task<IActionResult> Get(int id)
          {
            var hostel = await _Context.Hostels.Include(h=>h.Rooms)
            .FirstOrDefaultAsync(h=>h.HostelID==id);
            if (hostel==null) return NotFound();
            return Ok(_Mapper.Map<HostelRead>(hostel));
        }
        //Uploading Hostels
        [Authorize(Roles="LandLord,Admin")]
        [HttpPost("Upload")]
        public async Task<ActionResult<HostelRead>> UploadHostel(HostelCreate Upload)
        {
            var Hostel=_Mapper.Map<Hostel>(Upload);
            var landlordIdClaim= User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var AdminRole= User.IsInRole("Admin");
            if(landlordIdClaim == null || (landlordIdClaim != Upload.LandLordID.ToString() && !AdminRole))
            {
                return Forbid("You are not authorized to upload hostel for this landlord.");
            }

            if(!await _Context.LandLords.AnyAsync(l=>l.LandLordID==Upload.LandLordID))
            {
                return BadRequest("Invalid LandLordID. Landlord does not exist.");
            }

            if(string.IsNullOrEmpty(Upload.HostelName) || string.IsNullOrEmpty(Upload.HostelLocation))
            {
                return BadRequest("Name and Location are required");
            }

            if(_Context.Hostels.Any(h=>h.HostelName==Upload.HostelName && h.HostelLocation==Upload.HostelLocation))
            {
                return Conflict("Hostel with the same Name and Location already exists");
            }
            _Context.Hostels.Add(Hostel);
            await _Context.SaveChangesAsync();

           var HostelRead=_Mapper.Map<HostelRead>(Hostel);
            return CreatedAtAction(nameof(GetHostel),new{id=Hostel.HostelID}, HostelRead);
        }
        
     [Authorize(Roles = "LandLord,Admin")]
[HttpPut("{id}")]
public async Task<IActionResult> Update(int id, HostelCreate dto)
{
    var hostel = await _Context.Hostels.FindAsync(id);
    if (hostel == null)
        return NotFound("Hostel not found.");

    //AUTHORIZATION CHECK 
    var landlordIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    bool isAdmin = User.IsInRole("Admin");

    if (!isAdmin && landlordIdClaim != hostel.LandLordID.ToString())
        return Forbid("You are not authorized to edit this hostel.");

    // CONFLICT CHECK
    if (!string.IsNullOrEmpty(dto.HostelName) && !string.IsNullOrEmpty(dto.HostelLocation))
    {
        bool exists = await _Context.Hostels
            .AnyAsync(h =>
                h.HostelID != id &&
                h.HostelName == dto.HostelName &&
                h.HostelLocation == dto.HostelLocation
            );

        if (exists)
            return Conflict("Hostel with same Name and Location already exists.");
    }

    // ******** SAFE PARTIAL UPDATE ********
    hostel.HostelName = dto.HostelName ?? hostel.HostelName;
    hostel.HostelLocation = dto.HostelLocation ?? hostel.HostelLocation;
    hostel.HostelAddress = dto.HostelAddress ?? hostel.HostelAddress;
    hostel.TotalNumberOfRooms = dto.TotalNumberOfRooms;
    hostel.HostelPhoto = dto.HostelPhoto ?? hostel.HostelPhoto;
    hostel.HostelDocument = dto.HostelDocument ?? hostel.HostelDocument;
    hostel.Price = dto.Price;

    await _Context.SaveChangesAsync();

    return Ok("Hostel Updated Successfully");
}


        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) 
         {
            var hostel = await _Context.Hostels.FindAsync(id);
            if (hostel==null) return NotFound("Hostel not Found");
            _Context.Hostels.Remove(hostel);
            await _Context.SaveChangesAsync();
            return NoContent();
         }
    }
}
