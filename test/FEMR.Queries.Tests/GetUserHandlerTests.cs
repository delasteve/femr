using System;
using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;
using FEMR.TestHelpers.Builders;
using FluentAssertions;
using Moq;
using Xunit;

namespace FEMR.Queries.Tests
{
    public class GetUserInfoHandlerTests
    {
        [Fact]
        public async void Handle_GivenUserInRepository_ShouldReturnUserInfo()
        {
            var user = new UserBuilder()
                .WithUserId(Guid.NewGuid())
                .WithEmail("foo@gmail.com")
                .WithFirstName("Bob")
                .WithLastName("Dole")
                .Build();

            var mockAggregateRepository = new Mock<IAggregateRepository>();
            mockAggregateRepository
                .Setup(foo => foo.Demand<User>(It.Is<Guid>(s => s == user.Id)))
                .Returns(Task.FromResult(user));

            var handler = new GetUserInfoHandler(mockAggregateRepository.Object);
            var queryResult = await handler.Handle(new GetUserInfo(user.Id));
            queryResult.Should().BeOfType<UserInfo>();
            queryResult.UserId.Should().Be(user.Id);
            queryResult.Email.Should().Be(user.Email);
            queryResult.FirstName.Should().Be(user.FirstName);
            queryResult.LastName.Should().Be(user.LastName);

        }
    }
}
