using System.Threading.Tasks;

namespace FEMR.Core
{
    public interface ICommandHandler<in TCommand>
    {
        Task Handle(TCommand command);
    }
}
