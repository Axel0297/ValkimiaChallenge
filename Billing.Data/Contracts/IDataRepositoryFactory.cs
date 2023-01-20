using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Data.Contracts
{
    public interface IDataRepositoryFactory
    {
        IDataRepository<TEntity> Get<TEntity>() where TEntity : class, new();
    }
}
