namespace Hmssolution.HmsDTOs.HostelDTOs;

public record class HostelRead
{
    public int HostelID { get; set; }
    public string HostelName { get; set; } = string.Empty;
    public string HostelLocation { get; set; } = string.Empty;
    public string HostelAddress { get; set; } = string.Empty;
    public int TotalNumberOfRooms { get; set; }
    public int TotalNumberOfBookedRooms { get; set; }
    public int TotalNumberOfAvailableRooms { get; set; }
    public decimal Price{ get; set; }
}
