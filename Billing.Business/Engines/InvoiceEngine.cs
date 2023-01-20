using Billing.Business.Dto.Invoice;
using Billing.Data.Contracts;
using Billing.Data.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Billing.Business.Engines
{
    public class InvoiceEngine : IInvoiceEngine
    {
        private readonly IDataRepositoryFactory _dataRepositoryFactory;
        private readonly IMapper _mapper;

        public InvoiceEngine(IDataRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _dataRepositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async ValueTask<InvoiceResultDto> GetAsync(Guid id)
        {
            var repo = _dataRepositoryFactory.Get<Invoice>();
            var entity = await repo.GetAsync(x => x.Select(s => s)
                                                   .Include(i => i.Customer), w => w.Id == id);

            return _mapper.Map<InvoiceResultDto>(entity);
        }

        public async ValueTask<IEnumerable<InvoiceResultDto>> GetAllAsync(Guid customerId)
        {
            var repo = _dataRepositoryFactory.Get<Invoice>();
            var data = await repo.GetAllAsync(x => x.Select(s => s).Include(i => i.Customer), w => w.CustomerId == customerId);

            return data.Select(x => _mapper.Map<InvoiceResultDto>(x));
        }

        public async ValueTask<InvoiceResultDto> CreateInvoiceAsync(InvoicePayloadDto payload)
        {
            var repo = _dataRepositoryFactory.Get<Invoice>();
            var invoice = _mapper.Map<Invoice>(payload);

            var entityAdded = await repo.AddAsync(invoice);

            return _mapper.Map<InvoiceResultDto>(entityAdded);
        }
    }
}
