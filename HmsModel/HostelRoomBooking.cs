using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hmssolution.HmsModel;

public class HostelRoomBooking
{
    [Key]
    public int BookingID { get; set; }
    public string BookingStatus{get;set;}="Not Booked";
    //Relationships
    public int StudentID{ get; set; }
    public Student Student { get; set; } = null!;
    public string RoomNumber{get;set;}=null!;
    public int? HostelID{ get; set; }
    public Hostel Hostel { get; set; } = null!;
    public int? RoomId{get;set;}    
    public Room? Room {get;set;}
}
