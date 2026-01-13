using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hmssolution.HmsModel;

public class LandLord
{
    [Key]
    public int LandLordID { get; set; }
    [Required]
    public string FullName { get; set; } = string.Empty;
    [DataType(DataType.EmailAddress)]
    public string EmailAddress { get; set; } =string.Empty;
    [Required]
    public string Gender { get; set; } = string.Empty;
    [Required, DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;
    [Required, DataType(DataType.Upload)]
    public byte[] Verification { get; set; } = Array.Empty<byte>();
    public string VerificationStatus { get; set; } = "Not Verified";
    public List<Hostel> Hostels { get; set; } = new();

}
