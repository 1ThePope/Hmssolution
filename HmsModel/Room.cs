using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hmssolution.HmsModel;

public class Room
{
    [Key]
    public int RoomId{get;set;}
    [Required]
    public string RoomType{get;set;}=string.Empty;
    [Required]
    public byte[]? RoomPhoto{get;set;}
    public string RoomStatus{get;set;}="Available";
    // Relationship: A room belongs to ONE hostel
    [ForeignKey("Hostel")]
    public int HostelID{get;set;}
    public Hostel Hostel{get;set;}=null!;
    // A room can have many bookings over time
    public List<HostelRoomBooking> Bookings { get; set; } = new();
}
