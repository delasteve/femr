using System.Threading.Tasks;
using FEMR.Core;
using FEMR.DomainModels;
using FEMR.DomainModels.Boundaries;

namespace FEMR.Commands
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IAggregateRepository _aggregateRepository;
        private readonly IPasswordEncryptor _passwordEncryptor;
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IPasswordEncryptor passwordEncryptor, IAggregateRepository aggregateRepository, IUserRepository userRepository)
        {
            _passwordEncryptor = passwordEncryptor;
            _aggregateRepository = aggregateRepository;
            _userRepository = userRepository;
        }

        public async Task Handle(CreateUser command)
        {
            var hashedPassword = _passwordEncryptor.HashPassword(command.Password);

            var user = new User(command.UserId, command.Email, hashedPassword, command.FirstName, command.LastName);

            await _aggregateRepository.Save(user);
            await _userRepository.Save(user);
        }
    }
}
