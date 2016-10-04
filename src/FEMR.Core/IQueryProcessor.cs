using System.Threading.Tasks;

namespace FEMR.Core
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
