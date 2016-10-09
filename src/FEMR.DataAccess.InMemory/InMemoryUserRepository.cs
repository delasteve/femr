using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FEMR.DomainModels;
using FEMR.DomainModels.Boundaries;

namespace FEMR.DataAccess.InMemory
{
    public class InMemoryUserRepository : IUserRepository
    {
        private IList<User> _users;

        public InMemoryUserRepository()
        {
            _users = new List<User>();
        }

        public async Task<User> Demand(Guid userId)
        {
            return await Task.FromResult(_users.First(user => user.UserId == userId));
        }

        public async Task<User> DemandByEmail(string email)
        {
            return await Task.FromResult(_users.First(user => user.Email == email));
        }

        public async Task Save(User user)
        {
            _users.Add(user);

            await Task.FromResult(0);
        }
    }
}
