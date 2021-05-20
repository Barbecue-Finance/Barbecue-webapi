using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Common;
using Models.Db.MoneyOperations;
using Models.Db.OperationCategories;

namespace Models.Db
{
    public class Purse : IdEntity
    {
        [ForeignKey(nameof(Group))]
        public long GroupId { get; set; }

        public virtual Group Group { get; set; }

        public virtual ICollection<OutComeMoneyOperation> OutComingOperations { get; set; }
        public virtual ICollection<IncomeMoneyOperation> IncomingOperations { get; set; }

        public virtual ICollection<IncomeOperationCategory> IncomeOperationCategories { get; set; }
        public virtual ICollection<OutComeOperationCategory> OutComeOperationCategories { get; set; }
    }
}