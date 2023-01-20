using Billing.Business.Dto.City;
using Billing.Business.Dto.Customer;
using Billing.Business.Dto.Invoice;
using Billing.Business.Engines.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Business.Engines
{
    public interface ICustomerEngine : IBusinessEngine
    {
        ValueTask<CustomerResultDto> AuthenticateAsync(CustomerLoginPayloadDto payload);

        ValueTask<CustomerResultDto> GetAsync(Guid id);

        ValueTask<IEnumerable<CustomerResultDto>> GetAllAsync();

        ValueTask<CustomerResultDto> CreateCustomerAsync(CustomerPayloadDto payload);

        ValueTask<CustomerResultDto> UpdateCustomerAsync(CustomerPayloadDto payload);

        ValueTask<CustomerResultDto> DeleteCustomerAsync(Guid id);
    }
}
