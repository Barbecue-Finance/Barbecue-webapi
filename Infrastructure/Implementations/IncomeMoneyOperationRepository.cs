using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db.MoneyOperations;

namespace Infrastructure.Implementations
{
    public class IncomeMoneyOperationRepository : IdRepositoryBase<IncomeMoneyOperation>, IIncomeMoneyOperationRepository
    {
        public IncomeMoneyOperationRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}