using Models.Db;

namespace Models.DTOs.OperationCategories.Income
{
    public class CreateIncomeOperationCategoryDto
    {
        public string Title { get; set; }

        [Id(typeof(Purse))]
        public long PurseId { get; set; }
    }
}