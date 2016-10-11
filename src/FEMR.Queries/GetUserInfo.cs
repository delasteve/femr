using System;
using FEMR.Core;
using FEMR.DomainModels;

namespace FEMR.Queries
{
    public class GetUserInfo : IQuery<UserInfo>
    {
        public GetUserInfo(Guid userId)
        {
            UserId = userId;
        }

        public Guid UserId { get; }
    }
}
