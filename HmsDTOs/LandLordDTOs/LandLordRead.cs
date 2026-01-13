using Hmssolution.HmsDTOs.HostelDTOs;

namespace Hmssolution.HmsDTOs.LandLordDTOs;

public record class LandLordRead
{
    public int LandLordID { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string VerificationStatus { get; set; } = string.Empty;
    public List<HostelRead> Hostels { get; set; } = new();
}
