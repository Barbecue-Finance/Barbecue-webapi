using Infrastructure.Abstractions;
using Infrastructure.BaseImplementations;
using Models.Db.OperationCategories;

namespace Infrastructure.Implementations
{
    public class OutComeOperationCategoryRepository : IdRepositoryBase<OutComeOperationCategory>, IOutComeOperationCategoryRepository
    {
        public OutComeOperationCategoryRepository(BarbecueDbContext context) : base(context)
        {
        }
    }
}