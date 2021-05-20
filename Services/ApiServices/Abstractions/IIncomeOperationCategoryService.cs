using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs.OperationCategories.Income;

namespace Services.ApiServices.Abstractions
{
    public interface IIncomeOperationCategoryService : ICrudService<IncomeOperationCategoryWithIdDto, CreateIncomeOperationCategoryDto, UpdateIncomeOperationCategoryDto>
    {
        Task<ICollection<IncomeOperationCategoryWithIdDto>> GetByPurse(long id);
    }
}