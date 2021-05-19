using System.Threading.Tasks;
using Models.DTOs.Purses;

namespace Services.ApiServices.Abstractions
{
    public interface IPurseService
    {
        Task<PurseWithIdDto> GetById(long id);
    }
}