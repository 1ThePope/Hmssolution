namespace Hmssolution.HmsDTOs.Admin;

public record AdminRead
{
    public int AdminId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    // REMOVE Password
}