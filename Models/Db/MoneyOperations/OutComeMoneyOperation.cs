using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Account;
using Models.Db.OperationCategories;

namespace Models.Db.MoneyOperations
{
    public class OutComeMoneyOperation : MoneyOperation
    {
        [ForeignKey(nameof(OutComeOperationCategory))]
        public long OutComeOperationCategoryId { get; set; }

        public virtual OutComeOperationCategory OutComeOperationCategory { get; set; }

        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}