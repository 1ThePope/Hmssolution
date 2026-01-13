using Hmssolution.HmsModel;

namespace Hmssolution.HmsDTOs;

public record class StudentRead
{
    public int StudentID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string MatricNo { get; set; } = string.Empty;
    public int Level { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Faculty { get; set; } = string.Empty;
    public string Religion { get; set; } = string.Empty;
    public required List<HostelRoomBookingRead> Booking { get; set; }
}
