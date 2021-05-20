using System.Collections.Generic;
using Models.DTOs.OperationCategories;
using Models.DTOs.OperationCategories.Income;
using Models.DTOs.OperationCategories.OutCome;

namespace Models.DTOs.Purses
{
    public class PurseWithIdDto
    {
        public long Id { get; set; }
        
        public float Amount { get; set; }

        public ICollection<IncomeOperationCategoryWithIdDto> IncomeOperationCategories { get; set; }
        public ICollection<OutComeOperationCategoryWithIdDto> OutComeOperationCategories { get; set; }
    }
}