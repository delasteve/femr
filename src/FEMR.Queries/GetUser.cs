using System;
using FEMR.Core;
using FEMR.DomainModels;

namespace FEMR.Queries
{
    public class GetUser : IQuery<User>
    {
        public GetUser(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
