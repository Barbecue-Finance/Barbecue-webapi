using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.OperationCategories;

namespace Models.Db.MoneyOperations
{
    public class OutComeMoneyOperation : MoneyOperation
    {
        [ForeignKey(nameof(OutComeOperationCategory))]
        public long OutComeOperationCategoryId { get; set; }

        public virtual OutComeOperationCategory OutComeOperationCategory { get; set; }
    }
}