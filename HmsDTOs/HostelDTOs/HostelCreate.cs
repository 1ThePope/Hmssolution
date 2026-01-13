namespace Hmssolution.HmsDTOs.HostelDTOs;

public record class HostelCreate
{
    public string HostelName { get; set; } = string.Empty;
    public string HostelLocation { get; set; } = string.Empty;
    public string HostelAddress { get; set; } = string.Empty;
    public int TotalNumberOfRooms { get; set; }
    public byte[] HostelPhoto { get; set; } = Array.Empty<byte>();
    public byte[] HostelDocument { get; set; } = Array.Empty<byte>();
    public decimal Price{ get; set; }
    public int LandLordID { get; set; }
}
