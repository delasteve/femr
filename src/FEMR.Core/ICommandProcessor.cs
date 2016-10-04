using System.Threading.Tasks;

namespace FEMR.Core
{
    public interface ICommandProcessor
    {
        Task Process(ICommand command);
    }
}
