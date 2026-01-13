using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Hmssolution.DBContext;
using Hmssolution.HmsDTOs.RoomDTOs;
using Hmssolution.HmsModel;
using System.Security.Claims;

namespace Hmssolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly HmssolutionContext _context;
        private readonly IMapper _mapper;
        public RoomController(HmssolutionContext context, IMapper mapper)
         { 
            _context = context;
            _mapper = mapper;
     }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoomById(int id)
        {
            var room = await _context.Rooms
                .Include(r => r.Hostel)
                .FirstOrDefaultAsync(r => r.RoomId == id);

            if (room == null)
                return NotFound();

            return Ok(_mapper.Map<RoomRead>(room));
        }


        [Authorize(Roles = "LandLord,Admin")]
            [HttpPost]
            public async Task<IActionResult> Create(RoomCreate dto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                // Optional: Check hostel exists
                var hostel = await _context.Hostels.FindAsync(dto.HostelID);
                if (hostel == null)
                    return NotFound("Hostel not found");

                // Optional: Ownership check for LandLord
                if (User.IsInRole("LandLord"))
                {
                    var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    if (hostel.LandLordID.ToString() != userId)
                        return Forbid();
                }

                var room = _mapper.Map<Room>(dto);

                _context.Rooms.Add(room);
                await _context.SaveChangesAsync();

                var createdRoom = await _context.Rooms
                    .Include(r => r.Hostel)
                    .FirstOrDefaultAsync(r => r.RoomId == room.RoomId);

                return CreatedAtAction(
                    nameof(GetRoomById),
                    new { id = createdRoom!.RoomId },
                    _mapper.Map<RoomRead>(createdRoom)
                );
            }


        [Authorize(Roles="LandLord,Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null) return NotFound();
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
