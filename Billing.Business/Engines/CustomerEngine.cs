using Billing.Business.Dto.Customer;
using Billing.Business.Dto.Invoice;
using Billing.Data.Contracts;
using Billing.Data.Entities;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;

namespace Billing.Business.Engines
{
    public class CustomerEngine : ICustomerEngine
    {
        private readonly IDataRepositoryFactory _dataRepositoryFactory;
        private readonly IMapper _mapper;

        public CustomerEngine(IDataRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _dataRepositoryFactory = repositoryFactory;
            _mapper = mapper;
        }

        public async ValueTask<CustomerResultDto> AuthenticateAsync(CustomerLoginPayloadDto payload)
        {
            var repo = _dataRepositoryFactory.Get<Customer>();
            var data = await repo.GetAsync(x => x.Select(s => s), w => w.Email == payload.Email && w.Password == payload.Password);

            return _mapper.Map<CustomerResultDto>(data);
        }

        public async ValueTask<CustomerResultDto> GetAsync(Guid id)
        {
            var repo = _dataRepositoryFactory.Get<Customer>();
            var entity = await repo.GetAsync(x => x.Select(s => s)
                                                   .Include(i => i.City), w => w.Id == id);

            return _mapper.Map<CustomerResultDto>(entity);
        }

        public async ValueTask<IEnumerable<CustomerResultDto>> GetAllAsync()
        {
            var repo = _dataRepositoryFactory.Get<Customer>();
            var data = await repo.GetAllAsync(x => x.Select(s => s).Include(i => i.City), w => w.Enabled);

            return data.Select(x => _mapper.Map<CustomerResultDto>(x));
        }

        public async ValueTask<CustomerResultDto> CreateCustomerAsync(CustomerPayloadDto payload)
        {
            var repo = _dataRepositoryFactory.Get<Customer>();
            var customer = _mapper.Map<Customer>(payload);

            var entityAdded = await repo.AddAsync(customer);

            return _mapper.Map<CustomerResultDto>(entityAdded);
        }

        public async ValueTask<CustomerResultDto> UpdateCustomerAsync(CustomerPayloadDto payload)
        {
            var repo = _dataRepositoryFactory.Get<Customer>();
            var entityToModify = await repo.GetAsync(x => x, w => w.Id == payload.Id);

            entityToModify.Name = payload.Name;
            entityToModify.Lastname = payload.Lastname;
            entityToModify.Address = payload.Address;
            entityToModify.CityId = payload.CityId;

            var entityModificated = await repo.UpdateAsync(entityToModify);
            return _mapper.Map<CustomerResultDto>(entityModificated);
        }

        public async ValueTask<CustomerResultDto> DeleteCustomerAsync(Guid id)
        {
            var repo = _dataRepositoryFactory.Get<Customer>();
            var entityToDelete = await repo.GetAsync(x => x, w => w.Id == id);

            entityToDelete.Enabled = false;

            var entityDeleted = await repo.UpdateAsync(entityToDelete);
            return _mapper.Map<CustomerResultDto>(entityDeleted);
        }
    }
}
