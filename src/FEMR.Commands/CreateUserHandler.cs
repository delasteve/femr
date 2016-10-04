using System;
using System.Threading.Tasks;
using FEMR.Core;

namespace FEMR.Commands
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        public Task Handle(CreateUser command)
        {
            throw new NotImplementedException();
        }
    }
}
