using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db.OperationCategories;

namespace Infrastructure.Implementations
{
    public class IncomeOperationCategoryRepository : IdRepositoryBase<IncomeOperationCategory>, IIncomeOperationCategoryRepository
    {
        public IncomeOperationCategoryRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}