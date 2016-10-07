using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;

namespace FEMR.Queries
{
    public class GetUserHandler : IQueryHandler<GetUser, User>
    {
        private readonly IAggregateRepository _aggregateRepository;

        public GetUserHandler(IAggregateRepository aggregateRepository)
        {
            _aggregateRepository = aggregateRepository;
        }

        public Task<User> Handle(GetUser query)
        {
            return _aggregateRepository.Demand<User>(query.UserId);
        }
    }
}
