using System.Threading.Tasks;
using Infrastructure.BaseAbstractions;
using Models.Db.Account;

namespace Infrastructure.Abstractions
{
    using T = User;
    public interface IUserRepository  : IGetById<T>, IAdd<T>, IUpdate<T>, IRemove<T>, ICount<T>, IGetMany<T>
    {
        Task<T> GetByLogin(string login);
    }
}