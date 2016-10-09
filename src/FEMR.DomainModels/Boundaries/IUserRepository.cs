using System;
using System.Threading.Tasks;

namespace FEMR.DomainModels.Boundaries
{
    public interface IUserRepository
    {
        Task<User> Demand(Guid userId);
        Task<User> DemandByEmail(string Email);
        Task Save(User user);
    }
}
