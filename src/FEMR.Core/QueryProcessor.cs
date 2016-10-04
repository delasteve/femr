using System.Threading.Tasks;

namespace FEMR.Core
{
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceLocator _serviceLocator;

        public QueryProcessor(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public static IQueryProcessor Current { get; set; }

        public async Task<TResult> Process<TResult>(IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceLocator.GetInstance(handlerType);

            return await handler.Handle((dynamic)query);
        }
    }
}
