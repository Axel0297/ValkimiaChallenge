using Billing.Business.Dto.City;
using Billing.Business.Engines.Contracts;

namespace Billing.Business.Engines
{
    public interface ICityEngine : IBusinessEngine
    {
        ValueTask<IEnumerable<CityResultDto>> GetAllAsync();
    }
}
