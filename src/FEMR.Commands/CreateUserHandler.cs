using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;

namespace FEMR.Commands
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IAggregateRepository _aggregateRepository;
        private readonly IPasswordEncryptor _passwordEncryptor;

        public CreateUserHandler(IPasswordEncryptor passwordEncryptor, IAggregateRepository aggregateRepository)
        {
            _passwordEncryptor = passwordEncryptor;
            _aggregateRepository = aggregateRepository;
        }

        public async Task Handle(CreateUser command)
        {
            var hashedPassword = _passwordEncryptor.HashPassword(command.Password);

            await
                _aggregateRepository.Save(new User(command.UserId, command.Email, hashedPassword, command.FirstName,
                    command.LastName));
        }
    }
}
