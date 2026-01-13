namespace Hmssolution.HmsDTOs.RoomDTOs;

public record class RoomCreate
{
    public required int HostelID { get; set; }
    public required string RoomType { get; set; }
    public byte[]? RoomPhoto { get; set; }
}
