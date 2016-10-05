using System;
using FEMR.Core;

namespace FEMR.DomainModels.Events
{
    public class UserCreated : IEvent
    {
        public UserCreated(Guid userId, string email, string password, string firstName, string lastName)
        {
            UserId = userId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid UserId { get; }
        public string Email { get; }
        public string Password { get; }
        public string FirstName { get; }
        public string LastName { get; }
    }
}
