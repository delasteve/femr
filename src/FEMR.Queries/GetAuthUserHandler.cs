using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;
using FEMR.DomainModels.Boundaries;

namespace FEMR.Queries
{
    public class GetAuthUserHandler : IQueryHandler<GetAuthUser, User>
    {
        private readonly IUserRepository _userRepository;

        public GetAuthUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Handle(GetAuthUser query)
        {
            var user = await _userRepository.DemandByEmail(query.Email);

            return user;
        }
    }
}
