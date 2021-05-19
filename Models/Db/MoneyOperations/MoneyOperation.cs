using System;
using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Common;

namespace Models.Db.MoneyOperations
{
    public class MoneyOperation : IdEntity
    {
        public float Amount { get; set; }

        public string Comment { get; set; }

        public DateTime DateTime { get; set; }

        [ForeignKey(nameof(Purse))]
        public long PurseId { get; set; }

        public virtual Purse Purse { get; set; }
    }
}