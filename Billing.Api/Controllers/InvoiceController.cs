using Billing.Api.Exceptions;
using Billing.Api.Models.Invoice;
using Billing.Business.Dto.Invoice;
using Billing.Business.Engines;
using Billing.Business.Engines.Contracts;
using MapsterMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class InvoiceController : ApiControllerBase
    {
        private readonly IBusinessEngineFactory _businessEngineFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceController> _logger;

        public InvoiceController(ILogger<InvoiceController> logger, IBusinessEngineFactory businessEngineFactory, IMapper mapper)
        {
            _logger = logger;
            _businessEngineFactory = businessEngineFactory;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        public async Task<InvoiceResultDto> GetAsync(Guid id)
        {
            var engine = _businessEngineFactory.Get<IInvoiceEngine>();

            var invoice = await engine.GetAsync(id);

            return invoice;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IEnumerable<InvoiceResultDto>> GetAllAsync(Guid customerId)
        {
            var engine = _businessEngineFactory.Get<IInvoiceEngine>();

            var invoices = await engine.GetAllAsync(customerId);

            return invoices;
        }

        [HttpPost]
        [Route("")]
        [AllowAnonymous]
        public async Task<InvoiceResultDto> CreateInvoiceAsync([FromBody] InvoicePayloadViewModel model)
        {
            if (!ModelState.IsValid)
                throw new ModelValidationException("Error modelo CreateInvoiceAsync", GetModelErrors(ModelState));

            var payload = _mapper.Map<InvoicePayloadDto>(model);
            var service = _businessEngineFactory.Get<IInvoiceEngine>();

            var result = await service.CreateInvoiceAsync(payload);

            return result;
        }
    }
}
