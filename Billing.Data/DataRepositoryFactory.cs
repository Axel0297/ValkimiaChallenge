using Billing.Data.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Data
{
    public class DataRepositoryFactory : IDataRepositoryFactory
    {
        private readonly IServiceProvider? _Services;

        public DataRepositoryFactory() { }

        public DataRepositoryFactory(IServiceProvider services)
        {
            this._Services = services;
        }

        public IDataRepository<TEntity>? Get<TEntity>() where TEntity : class, new()
        {
            //Import instance of T from the DI container
            return _Services?.GetService<IDataRepository<TEntity>>();
        }
    }
}
