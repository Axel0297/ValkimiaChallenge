using Billing.Business.Dto.City;
using Billing.Data.Contracts;
using Billing.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Business.Engines
{
    public class CityEngine : ICityEngine
    {
        private readonly IDataRepositoryFactory _dataRepositoryFactory;

        public CityEngine(IDataRepositoryFactory repositoryFactory)
        {
            _dataRepositoryFactory = repositoryFactory;
        }

        public async ValueTask<IEnumerable<CityResultDto>> GetAllAsync()
        {
            var repo = _dataRepositoryFactory.Get<City>();
            var data = await repo.GetAllAsync(x => x.Select(s => new CityResultDto { Id = s.Id, Name = s.Name }));

            return data;
        }
    }
}
