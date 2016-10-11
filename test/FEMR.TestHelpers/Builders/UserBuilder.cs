using System;
using FEMR.DomainModels;

namespace FEMR.TestHelpers.Builders
{
    public class UserBuilder
    {
        private Guid _userId = Guid.NewGuid();
        private string _email = "";
        private string _password = "";
        private string _firstName = "";
        private string _lastName = "";

        public UserBuilder WithUserId(Guid userId)
        {
            _userId = userId;
            return this;
        }

        public UserBuilder WithEmail(string email)
        {
            _email = email;
            return this;
        }

        public UserBuilder WithPassword(string password)
        {
            _password = password;
            return this;
        }

        public UserBuilder WithFirstName(string firstName)
        {
            _firstName = firstName;
            return this;
        }

        public UserBuilder WithLastName(string lastName)
        {
            _lastName = lastName;
            return this;
        }

        public User Build()
        {
            return new User(_userId, _email, _password, _firstName, _lastName);
        }
    }
}
