using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs.Users;

namespace Services.ApiServices.Abstractions
{
    public interface IUserService : ICrudService<UserWithIdDto, CreateUserDto, UpdateUserDto>
    {
        Task<ICollection<UserWithIdDto>> GetByGroup(long id);
    }
}