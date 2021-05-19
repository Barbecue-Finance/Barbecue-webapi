using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class InviteRepository : IdRepositoryBase<Invite>, IInviteRepository
    {
        public InviteRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}