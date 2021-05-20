using System.Collections.Generic;
using System.Threading.Tasks;
using Models.DTOs.OperationCategories;
using Models.DTOs.OperationCategories.OutCome;

namespace Services.ApiServices.Abstractions
{
    public interface IOutComeOperationCategoryService : ICrudService<OutComeOperationCategoryWithIdDto, CreateOutComeOperationCategoryDto, UpdateOutComeOperationCategoryDto>
    {
        Task<ICollection<OutComeOperationCategoryWithIdDto>> GetByPurse(long id);
    }
}