using System;
using System.Linq;
using AutoMapper;
using Models.Db;
using Models.Db.Account;
using Models.Db.MoneyOperations;
using Models.Db.OperationCategories;
using Models.DTOs.Groups;
using Models.DTOs.Invites;
using Models.DTOs.MoneyOperations;
using Models.DTOs.MoneyOperations.Transfers;
using Models.DTOs.OperationCategories;
using Models.DTOs.OperationCategories.Income;
using Models.DTOs.OperationCategories.OutCome;
using Models.DTOs.Purses;
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

            CreateMap<Purse, PurseWithIdDto>()
                .ForMember(
                    dto => dto.Amount,
                    cfg => cfg.MapFrom(
                        p => p.IncomingOperations.Sum(o => o.Amount) - p.OutComingOperations.Sum(o => o.Amount)
                    )
                ).ReverseMap();

            CreateMap<Purse, IncomeOutcomeDto>()
                .ForMember(dto => dto.Incoming, cfg => cfg.MapFrom(p => p.IncomingOperations))
                .ForMember(dto => dto.OutComing, cfg => cfg.MapFrom(p => p.OutComingOperations));
            
            CreateMap<IncomeMoneyOperation, IncomeMoneyOperationDto>().ReverseMap();
            CreateMap<OutComeMoneyOperation, OutComeMoneyOperationDto>().ReverseMap();

            CreateMap<CreateMoneyOperationDto, IncomeMoneyOperation>().ReverseMap();
            CreateMap<CreateMoneyOperationDto, OutComeMoneyOperation>().ReverseMap();

            CreateMap<CreateTransferOperationDto, IncomeMoneyOperation>()
                .ForMember(o => o.PurseId, cfg => cfg.MapFrom(dto => dto.ToPurseId));

            CreateMap<CreateTransferOperationDto, OutComeMoneyOperation>()
                .ForMember(o => o.PurseId, cfg => cfg.MapFrom(dto => dto.FromPurseId));

            CreateMap<CreateIncomeOperationCategoryDto, IncomeOperationCategory>().ReverseMap();
            CreateMap<IncomeOperationCategoryWithIdDto, IncomeOperationCategory>().ReverseMap();
            CreateMap<UpdateIncomeOperationCategoryDto, IncomeOperationCategory>().ReverseMap();
            
            CreateMap<CreateOutComeOperationCategoryDto, OutComeOperationCategory>().ReverseMap();
            CreateMap<OutComeOperationCategoryWithIdDto, OutComeOperationCategory>().ReverseMap();
            CreateMap<UpdateOutComeOperationCategoryDto, OutComeOperationCategory>().ReverseMap();
        }
    }
}