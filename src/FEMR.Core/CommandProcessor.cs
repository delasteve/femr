using System.Threading.Tasks;

namespace FEMR.Core
{
    public class CommandProcessor : ICommandProcessor
    {
        private readonly IServiceLocator _serviceLocator;

        public CommandProcessor(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public async Task Process(ICommand command)
        {
            var handlerType = typeof(ICommandHandler<>).MakeGenericType(command.GetType());
            dynamic handler = _serviceLocator.GetInstance(handlerType);

            await handler.Handle((dynamic) command);
        }
    }
}
