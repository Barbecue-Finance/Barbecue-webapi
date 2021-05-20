using System.ComponentModel.DataAnnotations.Schema;
using Models.Db.Common;

namespace Models.Db.OperationCategories
{
    public class OperationCategory : IdEntity
    {
        public string Title { get; set; }

        [ForeignKey(nameof(Purse))]
        public long PurseId { get; set; }

        public virtual Purse Purse { get; set; }
    }
}