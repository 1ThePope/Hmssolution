using Hmssolution.HmsModel;
namespace Hmssolution.HmsDTOs.RoomDTOs;

public record class RoomRead
{
    public int RoomId{get;set;}
    public string RoomType{get;set;}=string.Empty;
    public byte[]? RoomPhoto{get;set;}
    public string RoomStatus{get;set;}="Available";
    public string HostelName { get; set; }=string.Empty;
    public int HostelID { get; set; }
    public int? StudentID { get; set; }
}
