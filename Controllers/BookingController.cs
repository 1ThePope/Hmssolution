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

namespace Hmssolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly HmssolutionContext _Context;
        private readonly IMapper _mapper;

        public BookingController(HmssolutionContext context, IMapper mapper)
        {
            _Context = context;
            _mapper = mapper;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<IActionResult> GetBookings()
        {
            var bookings = await _Context.HostelRoomBookings
                .Include(b => b.Student)
                .Include(b => b.Hostel)
                .Include(b => b.Room)
                .ToListAsync();

            return Ok(_mapper.Map<List<HostelRoomBookingRead>>(bookings));
        }

        // GET: api/Booking/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooking(int id)
        {
            var booking = await _Context.HostelRoomBookings
                .Include(b => b.Student)
                .Include(b => b.Hostel)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BookingID == id);

            if (booking == null)
                return NotFound("Booking not found");

            return Ok(_mapper.Map<HostelRoomBookingRead>(booking));
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> CreateBooking(HostelRoomBookingCreate bookingDto)
        {
            // Check if room is already booked
            var existingBooking = await _Context.HostelRoomBookings
                .FirstOrDefaultAsync(b => b.RoomId == bookingDto.RoomId && b.BookingStatus == "Booked");

            if (existingBooking != null)
                return BadRequest("Room is already booked");

            // Map DTO to Entity
            var booking = _mapper.Map<HostelRoomBooking>(bookingDto);
            booking.BookingStatus = "Booked";

            _Context.HostelRoomBookings.Add(booking);

            // Update hostel's booked rooms count
            var hostel = await _Context.Hostels.FindAsync(booking.HostelID);
            if (hostel != null)
            {
                hostel.TotalNumberOfBookedRooms += 1;
            }

            await _Context.SaveChangesAsync();

            // Return the newly created booking
            var result = _mapper.Map<HostelRoomBookingRead>(booking);
            return CreatedAtAction(nameof(GetBooking), new { id = booking.BookingID }, result);
        }

        // DELETE: api/Booking/{id}  (optional)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var booking = await _Context.HostelRoomBookings.FindAsync(id);
            if (booking == null)
                return NotFound("Booking not found");

            // Update hostel's booked rooms count
            var hostel = await _Context.Hostels.FindAsync(booking.HostelID);
            if (hostel != null)
                hostel.TotalNumberOfBookedRooms -= 1;

            _Context.HostelRoomBookings.Remove(booking);
            await _Context.SaveChangesAsync();

            return NoContent();
        }
    }
}
