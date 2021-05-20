using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.OperationCategories;

namespace Models.Db.MoneyOperations
{
    public class IncomeMoneyOperation : MoneyOperation
    {
        [ForeignKey(nameof(IncomeOperationCategory))]
        public long IncomeOperationCategoryId { get; set; }

        public virtual IncomeOperationCategory IncomeOperationCategory { get; set; }
    }
}