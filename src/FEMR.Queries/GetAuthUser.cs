using FEMR.Core;
using FEMR.DomainModels;

namespace FEMR.Queries
{
    public class GetAuthUser : IQuery<User>
    {
        public GetAuthUser(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}
