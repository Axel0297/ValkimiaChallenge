
namespace Billing.Business.Engines.Contracts
{
    public interface IBusinessEngineFactory
    {
        T Get<T>() where T : IBusinessEngine;
    }
}
