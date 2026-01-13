using System;
using System.ComponentModel.DataAnnotations;

namespace Hmssolution.HmsModel;

public class Student
{
    [Key]
    public int StudentID { get; set; }
    [Required]
    public string FullName { get; set; } = string.Empty;
    [Required, DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; } =string.Empty;
    [Required, DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required, DataType(DataType.Password)]
    public string Password{get;set;} =string.Empty;
    [Required]
    public string Gender { get; set; } = string.Empty;
    public string MatricNo { get; set; } = string.Empty;
    public int Level { get; set; }
    [Required]
    public string Department { get; set; } = string.Empty;
    [Required]
    public string Faculty { get; set; } = string.Empty;
    [Required]
    public string Religion { get; set; } = string.Empty;
    [Required]
    public byte[] Photo { get; set; } = Array.Empty<byte>();
    // Student can have only ONE active booking
    public List<HostelRoomBooking> Booking{ get; set; }=new();
}
