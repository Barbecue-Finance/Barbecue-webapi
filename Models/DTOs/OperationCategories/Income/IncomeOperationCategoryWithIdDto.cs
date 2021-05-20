namespace Models.DTOs.OperationCategories.Income
{
    public class IncomeOperationCategoryWithIdDto
    {
        public long Id { get; set; }
        
        public string Title { get; set; }

        public long PurseId { get; set; }
    }
}