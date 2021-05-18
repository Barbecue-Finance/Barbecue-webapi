using System.Threading.Tasks;
using Models.Db.Account;
using Models.Db.Sessions;
using Models.DTOs.Requests;
using Models.DTOs.Responses;

namespace Services.ApiServices.Abstractions
{
    public interface ITokenSessionService
    {
        Task<LoginResultDto> Login(LoginDto loginDto);

        Task<TokenSession> GetByToken(string token);
        
        Task<User> GetAccountByToken(string token);
        
        Task Logout(long workerId);
    }
}