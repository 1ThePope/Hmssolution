namespace Hmssolution.HmsDTOs.LandLordDTOs;

public record class LandLordCreate
{
    public required  string FullName { get; set; } 
    public required  string PhoneNumber { get; set; }  
    public required string EmailAddress { get; set; } 
    public required  string Gender { get; set; }
    public required string Password { get; set; } 
    public required byte[] VerificationDocument { get; set; } = Array.Empty<byte>();
}
