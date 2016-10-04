using System;
using FEMR.Core;

namespace FEMR.DomainModels.Events
{
    public class UserCreated : IEvent
    {
        public Guid UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public UserCreated(Guid userId, string email, string firstName, string lastName)
        {
            UserId = userId;
            Email = email;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
