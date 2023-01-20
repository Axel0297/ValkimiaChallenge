using Billing.Business.Engines.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Billing.Business.Engines
{
    /// <summary>
    /// Importable class that represents a factory of Engines
    /// </summary>
    public class BusinessEngineFactory : IBusinessEngineFactory
    {
        private readonly IServiceProvider _Services;

        public BusinessEngineFactory(IServiceProvider services)
        {
            this._Services = services;
        }

        public T Get<T>() where T : IBusinessEngine
        {
            //Import instance of T from the DI container
            return _Services.GetService<T>();
        }
    }
}
