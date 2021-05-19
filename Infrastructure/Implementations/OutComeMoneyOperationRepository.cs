using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db.MoneyOperations;

namespace Infrastructure.Implementations
{
    public class OutComeMoneyOperationRepository : IdRepositoryBase<OutComeMoneyOperation>, IOutComeMoneyOperationRepository
    {
        public OutComeMoneyOperationRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}