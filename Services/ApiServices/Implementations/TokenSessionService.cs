using System;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Infrastructure.Verbatims;
using Models.Db.Account;
using Models.Db.Sessions;
using Models.DTOs.Requests;
using Models.DTOs.Responses;
using Services.ApiServices.Abstractions;

namespace Services.ApiServices.Implementations
{
    public class TokenSessionService : ITokenSessionService
    {
        private readonly ITokenSessionRepository _tokenSessionRepository;
        private readonly IUserRepository _userRepository;

        public TokenSessionService(ITokenSessionRepository tokenSessionRepository, IUserRepository userRepository)
        {
            _tokenSessionRepository = tokenSessionRepository;
            _userRepository = userRepository;
        }

        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            var userAccount = await _userRepository.GetByLogin(loginDto.Login);

            if (userAccount == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            if (userAccount.Password != loginDto.Password)
            {
                throw new(MessagesVerbatim.PasswordInvalid);
            }

            var lastAccountTokenSession = await _tokenSessionRepository.GetLastByUser(userAccount.Id);

            if (lastAccountTokenSession != null)
            {
                // if last session is not expired
                if (lastAccountTokenSession.EndDate > DateTime.Now)
                {
                    // return it's token
                    return new LoginResultDto(userAccount.Id, lastAccountTokenSession.Token);
                }

                await Logout(userAccount.Id);
            }

            // Create new Token Session

            var endDate = DateTime.Now.AddDays(1);

            TokenSession session = new()
            {
                User = userAccount,
                Token = Guid.NewGuid().ToString(),
                StartDate = DateTime.Now,
                EndDate = endDate
            };

            await _tokenSessionRepository.Add(session);

            // Save token session relation to user
            
            return new LoginResultDto(userAccount.Id, session.Token);
        }

        public async Task<TokenSession> GetByToken(string token)
        {
            return await _tokenSessionRepository.GetByToken(token);
        }

        public async Task<User> GetAccountByToken(string token)
        {
            var tokenSession = await _tokenSessionRepository.GetByToken(token);

            var userAccount = await _userRepository.GetById(tokenSession.UserAccountId);

            if (userAccount == null)
            {
                throw new(MessagesVerbatim.AuthTokenUnknown);
            }

            return userAccount;
        }

        public async Task Logout(long workerId)
        {
            var tokenSession = await _tokenSessionRepository.GetLastByUser(workerId);

            if (tokenSession == null)
            {
                throw new(MessagesVerbatim.AccountNotFound);
            }

            tokenSession.EndDate = DateTime.Now;
            await _tokenSessionRepository.Update(tokenSession);
        }
    }
}