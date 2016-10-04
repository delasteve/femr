using System;
using FEMR.DomainModels.Tests.Builders;
using FluentAssertions;
using Xunit;

namespace FEMR.DomainModels.Tests
{
    public class UserTests
    {
        [Fact]
        public void Create_SetsProperties()
        {
            var userId = Guid.NewGuid();
            const string email = "foo@gmail.com";
            const string firstName = "John";
            const string lastName = "Doe";

            var user = new User(userId, email, firstName, lastName);

            user.UserId.Should().Be(userId);
            user.Email.Should().Be(email);
            user.FirstName.Should().Be(firstName);
            user.LastName.Should().Be(lastName);
        }

        [Fact]
        public void UpdateEmail_UpdatesEmailProperty()
        {
            const string updatedEmail = "foo.bar@gmail.com";

            var user = new UserBuilder()
                .WithEmail("foo@gmail.com")
                .Build();

            user.UpdateEmail(updatedEmail);

            user.Email.Should().Be(updatedEmail);
        }
    }
}
