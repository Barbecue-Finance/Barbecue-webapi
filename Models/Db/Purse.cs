using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Common;
using Models.Db.MoneyOperations;

namespace Models.Db
{
    public class Purse : IdEntity
    {
        [ForeignKey(nameof(UserGroup))]
        public long UserGroupId { get; set; }
        public virtual UserGroup UserGroup { get; set; }
        
        public virtual ICollection<OutComeMoneyOperation> OutComingOperations { get; set; }
        public virtual ICollection<IncomeMoneyOperation> IncomingOperations { get; set; }
    }
}