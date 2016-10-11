using System.Threading.Tasks;
using FEMR.DomainModels.Boundaries;
using FEMR.TestHelpers.Builders;
using FluentAssertions;
using Moq;
using Xunit;

namespace FEMR.Queries.Tests
{
    public class GetAuthUserHandlerTests
    {
        [Fact]
        public async void Handle_GivenUserInRepository_ReturnsUser()
        {
            var user = new UserBuilder()
                .WithEmail("foo@gmail.com")
                .Build();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository
                .Setup(foo => foo.DemandByEmail(It.Is<string>(s => s == "foo@gmail.com")))
                .Returns(Task.FromResult(user));

            var handler = new GetAuthUserHandler(mockUserRepository.Object);
            var queryResult = await handler.Handle(new GetAuthUser("foo@gmail.com"));
            queryResult.Should().Be(user);
        }
    }
}
