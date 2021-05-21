using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;
using Models.Db.OperationCategories;

namespace Models.Db.MoneyOperations
{
    public class IncomeMoneyOperation : MoneyOperation
    {
        [ForeignKey(nameof(IncomeOperationCategory))]
        public long IncomeOperationCategoryId { get; set; }

        public virtual IncomeOperationCategory IncomeOperationCategory { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}