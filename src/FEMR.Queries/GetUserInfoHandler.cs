using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;

namespace FEMR.Queries
{
    public class GetUserInfoHandler : IQueryHandler<GetUserInfo, UserInfo>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public GetUserInfoHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public async Task<UserInfo> Handle(GetUserInfo query)
        {
            var user = await _aggregateRepository.Demand<User>(query.UserId);

            return new UserInfo(user.UserId, user.Email, user.FirstName, user.LastName);
        }
    }
}
