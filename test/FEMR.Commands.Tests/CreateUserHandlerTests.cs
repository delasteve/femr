using System;
using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;
using FEMR.DomainModels.Boundaries;
using FluentAssertions;
using Moq;
using Xunit;

namespace FEMR.Commands.Tests
{
    public class CreateUserHandlerTests
    {
        [Fact]
        public async void Handle_WillEncryptUsersPassword()
        {
            var mockPasswordEncryptor = new Mock<IPasswordEncryptor>();
            var mockAggregateRepository = new Mock<IAggregateRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var handler = new CreateUserHandler(mockPasswordEncryptor.Object, mockAggregateRepository.Object, mockUserRepository.Object);

            await handler.Handle(new CreateUser(Guid.NewGuid(), "foo@bar.com", "password", "first", "last"));

            mockPasswordEncryptor.Verify(encryptor => encryptor.HashPassword(It.IsAny<string>()), Times.Exactly(1));
        }

        [Fact]
        public async void Handle_WillCallSaveOnTheAggregateRepository()
        {
            var mockPasswordEncryptor = new Mock<IPasswordEncryptor>();
            var mockAggregateRepository = new Mock<IAggregateRepository>();
            var mockUserRepository = new Mock<IUserRepository>();

            var handler = new CreateUserHandler(mockPasswordEncryptor.Object, mockAggregateRepository.Object, mockUserRepository.Object);

            await handler.Handle(new CreateUser(Guid.NewGuid(), "foo@bar.com", "password", "first", "last"));

            mockAggregateRepository.Verify(repository => repository.Save(It.IsAny<User>()), Times.Exactly(1));
        }

        [Fact]
        public async void Handle_WillEncryptTheUsersPasswordThenCallSave()
        {
            var mockPasswordEncryptor = new Mock<IPasswordEncryptor>();
            var mockAggregateRepository = new Mock<IAggregateRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockSequence = new MockSequence();

            var callOrder = 0;

            mockPasswordEncryptor.InSequence(mockSequence)
                .Setup(encryptor => encryptor.HashPassword(It.IsAny<string>()))
                .Returns("foo")
                .Callback(() =>
                {
                    callOrder.Should().Be(0);
                    callOrder++;
                });
            mockAggregateRepository.InSequence(mockSequence)
                .Setup(repository => repository.Save(It.IsAny<User>()))
                .Returns(Task.CompletedTask)
                .Callback(() =>
                {
                    callOrder.Should().BeGreaterOrEqualTo(1);
                    callOrder++;
                });

            var handler = new CreateUserHandler(mockPasswordEncryptor.Object, mockAggregateRepository.Object, mockUserRepository.Object);

            await handler.Handle(new CreateUser(Guid.NewGuid(), "foo@bar.com", "password", "first", "last"));
        }
    }
}
