using System;

namespace FEMR.DomainModels
{
    public class UserInfo
    {
        public UserInfo(Guid userId, string email, string firstName, string lastName)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
