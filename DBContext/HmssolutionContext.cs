using System;
using Hmssolution.HmsModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Hmssolution.DBContext{

public class HmssolutionContext:IdentityDbContext<IdentityUser>
{
    public HmssolutionContext(DbContextOptions<HmssolutionContext> options) : base(options){}
    public DbSet<LandLord> LandLords { get; set; } = null!;
    public DbSet<Hostel> Hostels { get; set; }=null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<HostelRoomBooking> HostelRoomBookings { get; set; } = null!;
    public DbSet<Room> Rooms{get;set;}=null!;
    public DbSet<Admin> Admins{get;set;}=null!;
    public DbSet<PendingUser> PendingUsers{get;set;}=null!; 
    
protected override void OnModelCreating(ModelBuilder builder)
{
    base.OnModelCreating(builder);
}
}
}