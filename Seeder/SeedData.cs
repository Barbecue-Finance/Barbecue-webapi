using System;
using System.Threading.Tasks;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Models.Db;
using Models.DTOs.Groups;
using Models.DTOs.MoneyOperations;
using Models.DTOs.MoneyOperations.Transfers;
using Models.DTOs.Users;
using Services.ApiServices.Abstractions;

namespace Seeder
{
    public class SeedData
    {
        private IServiceScope _serviceScope;

        private IUserService _userService;

        private IGroupService _groupService;
        private IPurseService _purseService;

        private IMoneyOperationService _moneyOperationService;

        public SeedData(IServiceProvider provider)
        {
            _serviceScope = provider.CreateScope();
            Context = _serviceScope.ServiceProvider.GetRequiredService<BarbecueDbContext>();

            _userService = _serviceScope.ServiceProvider.GetRequiredService<IUserService>();
            _groupService = _serviceScope.ServiceProvider.GetRequiredService<IGroupService>();
            _purseService = _serviceScope.ServiceProvider.GetRequiredService<IPurseService>();
            _moneyOperationService = _serviceScope.ServiceProvider.GetRequiredService<IMoneyOperationService>();
        }

        ~SeedData()
        {
            _serviceScope.Dispose();
        }

        private BarbecueDbContext Context { get; set; }

        public async Task Seed()
        {
            await Context.Database.EnsureDeletedAsync();
            await Context.Database.EnsureCreatedAsync();

            Console.WriteLine("Database dropped and recreated");

            long user1Id = await _userService.Create(new CreateUserDto()
            {
                Login = "User1",
                Password = Utils.CreateMD5("User1"),
                Username = "Unique Username1"
            });

            long user2Id = await _userService.Create(new CreateUserDto()
            {
                Login = "User2",
                Password = Utils.CreateMD5("User2"),
                Username = "Unique Username2"
            });

            long group1Id = await _groupService.Create(new CreateGroupDto()
            {
                Title = "Тестовая группа 1",
                CreatorId = user1Id,
                Type = GroupType.Classic
            });

            long event1Id = await _groupService.Create(new CreateGroupDto()
            {
                Title = "Тестовое событие 1",
                CreatorId = user1Id,
                Type = GroupType.Event
            });

            long group2Id = await _groupService.Create(new CreateGroupDto()
            {
                Title = "Тестовая группа 2",
                CreatorId = user2Id,
                Type = GroupType.Classic
            });

            long personalId = await _groupService.Create(new CreateGroupDto()
            {
                Title = "Личное",
                CreatorId = user2Id,
                Type = GroupType.Classic
            });

            long personalPurseId = (await _purseService.GetByGroup(personalId)).Id;

            await _moneyOperationService.CreateIncome(new CreateMoneyOperationDto()
            {
                Amount = 100,
                Comment = "Мама дала 100р",
                OperationCategoryTitle = "Карманные",
                PurseId = personalPurseId,
                UserId = user2Id
            });
            await _moneyOperationService.CreateOutCome(new CreateMoneyOperationDto()
            {
                Amount = 50,
                Comment = "Купил чикенбургер",
                OperationCategoryTitle = "Фастфуд",
                PurseId = personalPurseId,
                UserId = user2Id
            });
            await _moneyOperationService.CreateOutCome(new CreateMoneyOperationDto()
            {
                Amount = 500,
                Comment = "Нашёл на улице",
                OperationCategoryTitle = "Повезло",
                PurseId = personalPurseId,
                UserId = user2Id
            });


            long shashlikiId = await _groupService.Create(new CreateGroupDto()
            {
                Title = "Шашлыки",
                CreatorId = user2Id,
                Type = GroupType.Classic
            });

            long shashlikiPurseId = (await _purseService.GetByGroup(shashlikiId)).Id;

            await _moneyOperationService.CreateIncome(new CreateMoneyOperationDto()
            {
                Amount = 500,
                Comment = "Скромная сумма",
                OperationCategoryTitle = "Мясо",
                PurseId = shashlikiPurseId,
                UserId = user2Id
            });
            await _moneyOperationService.CreateIncome(new CreateMoneyOperationDto()
            {
                Amount = 200,
                Comment = "Скромная сумма",
                OperationCategoryTitle = "Овощи",
                PurseId = shashlikiPurseId,
                UserId = user2Id
            });
            await _moneyOperationService.CreateIncome(new CreateMoneyOperationDto()
            {
                Amount = 200,
                Comment = "Скромная сумма",
                OperationCategoryTitle = "Фрукты",
                PurseId = shashlikiPurseId,
                UserId = user2Id
            });

            var purse1Id = (await _purseService.GetByGroup(group1Id)).Id;
            var purse2Id = (await _purseService.GetByGroup(group2Id)).Id;

            await _moneyOperationService.CreateIncome(new CreateMoneyOperationDto()
            {
                Amount = 100,
                Comment = "Тестовое пополнение1",
                PurseId = purse1Id,
                OperationCategoryTitle = "Зарплата",
                UserId = user1Id
            });
            await _moneyOperationService.CreateTransfer(new CreateTransferOperationDto()
            {
                Amount = 10,
                Comment = "Тестовый трансфер",
                FromPurseId = purse1Id,
                ToPurseId = purse2Id,
                UserId = user1Id
            });
            await _moneyOperationService.CreateOutCome(new CreateMoneyOperationDto()
            {
                Amount = 1000,
                Comment = "Тестовое снятие",
                PurseId = purse2Id,
                OperationCategoryTitle = "Шашлыки",
                UserId = user2Id
            });
        }
    }
}