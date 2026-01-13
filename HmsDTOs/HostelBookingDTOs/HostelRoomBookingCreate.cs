namespace Hmssolution.HmsDTOs;

public record class HostelRoomBookingCreate
{
    public int StudentID { get; set; }
    public int HostelID { get; set; }
    public int RoomId { get; set; }
    public string? RoomNumber { get; set; }
}
