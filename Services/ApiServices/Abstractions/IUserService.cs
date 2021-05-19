using Models.DTOs.Users;

namespace Services.ApiServices.Abstractions
{
    public interface IUserService : ICrudService<UserWithIdDto, CreateUserDto, UpdateUserDto>
    {
    }
}