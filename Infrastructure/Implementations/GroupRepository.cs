using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db;

namespace Infrastructure.Implementations
{
    public class GroupRepository : IdRepositoryBase<Group>, IGroupRepository
    {
        public GroupRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}