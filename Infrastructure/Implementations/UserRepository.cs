using System.Threading.Tasks;
using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Microsoft.EntityFrameworkCore;
using Models.Db.Account;

namespace Infrastructure.Implementations
{
    public class UserRepository : IdRepositoryBase<User>, IUserRepository
    {
        public UserRepository(BarbecueDbContext context) : base(context)
        {
        }

        public async Task<User> GetByLogin(string login)
        {
            return await Context.UserAccounts.FirstOrDefaultAsync(wa=>wa.Login == login);
        }
    }
}