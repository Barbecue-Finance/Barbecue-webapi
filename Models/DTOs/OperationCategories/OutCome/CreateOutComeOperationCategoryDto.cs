using Models.Db;

namespace Models.DTOs.OperationCategories.OutCome
{
    public class CreateOutComeOperationCategoryDto
    {
        public string Title { get; set; }

        [Id(typeof(Purse))]
        public long PurseId { get; set; }
    }
}