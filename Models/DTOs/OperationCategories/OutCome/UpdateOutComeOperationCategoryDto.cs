using Models.Db;
using Models.Db.OperationCategories;

namespace Models.DTOs.OperationCategories.OutCome
{
    public class UpdateOutComeOperationCategoryDto
    {
        [Id(typeof(OperationCategory))]
        public long Id { get; set; }
        
        public string Title { get; set; }

        [Id(typeof(Purse))]
        public long PurseId { get; set; }
    }
}