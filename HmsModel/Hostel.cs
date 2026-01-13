using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hmssolution.HmsModel;

public class Hostel
{
    [Key]
    public int HostelID { get; set; }
    [Required]
    public string HostelName { get; set; } = string.Empty;
    [Required]
    public string HostelLocation { get; set; } = string.Empty;
    [Required]
    public string HostelAddress { get; set; } = string.Empty;
    public int TotalNumberOfRooms { get; set; }
    public int TotalNumberOfBookedRooms { get; set; }
    [NotMapped]
    public int TotalNumberOfAvailableRooms => TotalNumberOfRooms - TotalNumberOfBookedRooms;
    [Required, DataType(DataType.Upload)]
    public byte[]? HostelPhoto { get; set; }
     [Required, DataType(DataType.Upload)]
    public byte[]? HostelDocument { get; set; }
    [Required]
    public string Verification { get; set; } = "Not verified";
    [Required,Column(TypeName="decimal(18,2)")]
    public decimal Price { get; set; }

    [ForeignKey("LandLord")]
    public int LandLordID { get; set; }
    public LandLord? LandLord { get; set; }
    public List<Room>? Rooms{get;set;}=new();
    public List<HostelRoomBooking> Bookings { get; set; } = new();

    
}
