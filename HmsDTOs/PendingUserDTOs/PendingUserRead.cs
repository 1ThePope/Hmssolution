namespace Hmssolution.HmsDTOs.PendingUserDTOs;

public record class PendingUserRead
{
    public int Id { get; set; }
    public required string Role { get; set; } // e.g., "Student", "Landlord", "Agent", "Admin"
    public required string EmailAddress { get; set; }  
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; }
    public string? Faculty { get; set; }
    public string? Department { get; set; }
    public string? MatricNo { get; set; }
    public int Level { get; set; }
    public string? Gender { get; set; }
    public string? Religion { get; set; }
    //public DateTime OtpExpiry { get; set; }
    public DateTime CreatedAt { get; set; }
}
