using System.Threading.Tasks;

namespace FEMR.Core
{
    public interface IMessenger
    {
        Task Send(object message);
    }
}
