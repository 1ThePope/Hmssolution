/*
using AutoMapper;
using Hmssolution.DBContext;
using Hmssolution.HmsDTOs.PendingUserDTOs;
using Hmssolution.HmsModel;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Hmssolution.Controllers.Verification
{
    [Route("api/[controller]")]
    [ApiController]
    public class PendingUserController : ControllerBase
    {
        public readonly HmssolutionContext _Context;
        public readonly IMapper _Mapper;
        public readonly IMailService _Emailservice;
        public PendingUserController(HmssolutionContext context, IMapper mapper, IMailService emailService)
        {
            _Context = context;
            _Mapper = mapper;
            _Emailservice = emailService;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<PendingUserRead>>CreatePendingUser(PendingUserCreate users)
        {
            if (await _Context.PendingUsers.AnyAsync(u => u.EmailAddress == users.EmailAddress))
                return BadRequest("Email Verfication Pending");
            var PendingUser = _Mapper.Map<PendingUser>(users);
            _Context.PendingUsers.Add(PendingUser);
            await _Context.SaveChangesAsync();

            PendingUser.OtpCode=GenerateOtp();
            PendingUser.OtpExpiry=DateTime.Now.AddMinutes(5);
            PendingUser.CreatedAt=DateTime.Now;

            _Emailservice.SendOtpAsync(PendingUser.EmailAddress,PendingUser.OtpCode);
            
            var Readdto = new PendingUserRead
            {
                Id=PendingUser.Id,
                Role=PendingUser.Role,
                EmailAddress=PendingUser.EmailAddress,
                OtpExpiry=PendingUser.OtpExpiry,
                CreatedAt=PendingUser.CreatedAt
            };
            return Ok(Readdto);
        }
        private string GenerateOtp()
            {
                var rng = new Random();
                return rng.Next(100000, 999999).ToString(); // 6-digit OTP
            }
    }
}
*/