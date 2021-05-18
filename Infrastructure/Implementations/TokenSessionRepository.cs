using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Microsoft.EntityFrameworkCore;
using Models.Db.Sessions;

namespace Infrastructure.Implementations
{
    public class TokenSessionRepository : IdRepositoryBase<TokenSession>, ITokenSessionRepository
    {
        public TokenSessionRepository(BarbecueDbContext context) : base(context)
        {
        }

        public async Task<TokenSession> GetByToken(string token)
        {
            return await Context.TokenSessions.FirstOrDefaultAsync(ts => ts.Token == token);
        }

        public async Task<TokenSession> GetLastByUser(long userId)
        {
            return await Context.TokenSessions.Where(ts => ts.UserAccountId == userId).OrderBy(ts => ts.Id).LastOrDefaultAsync();
        }
    }
}