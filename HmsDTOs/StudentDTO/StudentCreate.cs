namespace Hmssolution.HmsDTOs;

public record class StudentCreate
{
    public required string FullName { get; set; }
    public required string PhoneNumber { get; set; }
    public required string EmailAddress { get; set; }
    public required string Gender { get; set; }
    public required string MatricNo { get; set; }
    public required int Level { get; set; }
    public required string Department { get; set; }
    public required string Faculty { get; set; }
    public required string Religion { get; set; }
    public required string Password { get; set; }
}
