using System;
using AutoMapper;
using Hmssolution.HmsDTOs;
using Hmssolution.HmsDTOs.Admin;
using Hmssolution.HmsDTOs.HostelDTOs;
using Hmssolution.HmsDTOs.LandLordDTOs;
using Hmssolution.HmsDTOs.PendingUserDTOs;
using Hmssolution.HmsDTOs.RoomDTOs;
using Hmssolution.HmsModel;

namespace Hmssolution.Mapping;

public class MappingProfiles:Profile
{
    public MappingProfiles()
    {
        //LandLord
        CreateMap<LandLord, LandLordRead>();
        CreateMap<LandLordCreate, LandLord>(); 
        // Hostel
        CreateMap<Hostel, HostelRead>()
            .ForMember(dest => dest.TotalNumberOfAvailableRooms,
                   opt => opt.MapFrom(src => src.TotalNumberOfRooms - src.TotalNumberOfBookedRooms));
        CreateMap<HostelCreate, Hostel>()
        .ForMember(dest => dest.HostelID, opt => opt.Ignore())
        .ForMember(dest => dest.Rooms, opt => opt.Ignore())
        .ForMember(dest => dest.Bookings, opt => opt.Ignore());;

        // Student
        CreateMap<Student, StudentRead>();

        // HostelRoomBooking
        CreateMap<HostelRoomBookingCreate, HostelRoomBooking>()
        .ForMember(dest => dest.BookingID, opt=>opt.Ignore())
        .ForMember(dest => dest.Student, opt => opt.Ignore())
        .ForMember(dest => dest.Hostel, opt => opt.Ignore())
        .ForMember(dest => dest.Room, opt => opt.Ignore());

        CreateMap<HostelRoomBooking, HostelRoomBookingRead>();

        //Room
        CreateMap<RoomCreate,Room>()
            .ForMember(dest => dest.RoomId, opt => opt.Ignore())
            .ForMember(dest => dest.Bookings, opt => opt.Ignore());
        CreateMap<Room,RoomRead>()
         .ForMember(dest => dest.HostelName, opt => opt.MapFrom(src => src.Hostel.HostelName));

        //Admin
        CreateMap<AdminCreate,Admin>()
         .ForMember(dest => dest.AdminId, opt => opt.Ignore());

        CreateMap<Admin,AdminRead>();
        
        //PendingUser
       CreateMap<PendingUserCreate, PendingUser>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => "Pending"))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<PendingUser, PendingUserRead>();
    }
}
