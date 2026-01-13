using System;
using System.ComponentModel.DataAnnotations;

namespace Hmssolution.HmsModel;

public class PendingUser
{
    [Key]
    public int Id { get; set; }

    // Student or LandLord
    [Required]
    public string Role { get; set; } = string.Empty;

    // Basic info
    [Required, EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    // Only students will use these
    public string? Faculty { get; set; }
    public string? Department { get; set; }
    public string? MatricNo { get; set; }
    public int Level { get; set; }
    public string? Religion { get; set; }

    // Common fields
    public string? Gender { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    // OTP system
    public string Status { get; set; } = "Pending"; // Pending, Verified, Expired

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
