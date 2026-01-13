using System;
using System.ComponentModel.DataAnnotations;

namespace Hmssolution.HmsModel;

public class Admin
{
    [Key]
    public int AdminId{get;set;}
    [Required]
    public string? FullName{get;set;}
    [Required,DataType(DataType.EmailAddress)]
    public string? EmailAddress{get;set;}
    public string? Password{get;set;}
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber{get;set;}
}
