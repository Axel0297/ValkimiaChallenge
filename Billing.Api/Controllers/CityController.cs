using Billing.Business.Dto.City;
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
    public class CityController : ApiControllerBase
    {
        private readonly IBusinessEngineFactory _businessEngineFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<CityController> _logger;

        public CityController(ILogger<CityController> logger, IBusinessEngineFactory businessEngineFactory, IMapper mapper)
        {
            _logger = logger;
            _businessEngineFactory = businessEngineFactory;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("")]
        [AllowAnonymous]
        public async Task<IEnumerable<CityResultDto>> GetAllAsync()
        {
            var engine = _businessEngineFactory.Get<ICityEngine>();

            var cities = await engine.GetAllAsync();

            return cities;
        }
    }
}
