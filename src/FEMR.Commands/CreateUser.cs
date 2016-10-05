using System;
using FEMR.Core;

namespace FEMR.Commands
{
    public class CreateUser : ICommand
    {
        public CreateUser(Guid userId, string email, string password, string firstName, string lastName)
        {
            UserId = userId;
            Email = email;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

        public Guid UserId { get; }
        public string Email { get; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Password { get; }
    }
}
