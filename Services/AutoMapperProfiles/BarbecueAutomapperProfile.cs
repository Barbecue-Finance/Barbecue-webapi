using System;
using AutoMapper;
using Models.Db;
using Models.Db.Account;
using Models.DTOs.Groups;
using Models.DTOs.Invites;
using Models.DTOs.Users;

namespace Services.AutoMapperProfiles
{
    // --------------------------------------------------------- //
    // EVEN IF YOUR IDE SAYS THIS CODE IS UNUSED, DONT DELETE IT //
    // --------------------------------------------------------- //

    public class BarbecueAutomapperProfile : Profile
    {
        public BarbecueAutomapperProfile()
        {
            // ReverseMap() нужен для обратной конвертации любого мапа

            CreateMap<string, TimeSpan>().ConvertUsing(s => TimeSpan.ParseExact(s, "hh\\:mm", null));
            CreateMap<TimeSpan, string>().ConvertUsing(time => $"{time.Hours:00}:{time.Minutes:00}");

            // -----------
            // This doesn't work for some reason, need to specify every derived type
            // CreateMap<LatLngDto, LatLng>().ReverseMap();
            // -----------

            CreateMap<User, CreateUserDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<User, UserWithIdDto>().ReverseMap();

            CreateMap<Invite, InviteWithIdDto>().ReverseMap();
            CreateMap<Invite, CreateInviteDto>().ReverseMap();

            CreateMap<Group, GroupWithIdDto>().ReverseMap();
            CreateMap<Group, CreateGroupDto>().ReverseMap();
        }
    }
}