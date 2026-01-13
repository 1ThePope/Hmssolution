using Hmssolution.HmsDTOs.HostelDTOs;
using Hmssolution.HmsModel;

namespace Hmssolution.HmsDTOs;

public record class HostelRoomBookingRead
{
    public int BookingID { get; set; }
    public string? RoomNumber { get; set; }
    public int StudentID { get; set; }
    public int HostelID { get; set; }
    public int? RoomId { get; set; }
}
