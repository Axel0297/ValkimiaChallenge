using Billing.Business.Dto.Invoice;
using Billing.Business.Engines.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Business.Engines
{
    public interface IInvoiceEngine : IBusinessEngine
    {
        ValueTask<InvoiceResultDto> GetAsync(Guid id);

        ValueTask<IEnumerable<InvoiceResultDto>> GetAllAsync(Guid customerId);

        ValueTask<InvoiceResultDto> CreateInvoiceAsync(InvoicePayloadDto payload);
    }
}
