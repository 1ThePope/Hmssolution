namespace Hmssolution.HmsDTOs.Admin;

public record AdminCreate
{
    public required string FullName { get; set; } 
    public required string EmailAddress { get; set; } 
    public string? PhoneNumber { get; set; } 
    public required string Password { get; set; } // OK here, hashed later
}

