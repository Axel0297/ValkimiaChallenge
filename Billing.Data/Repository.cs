using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billing.Data
{
    public class Repository<TEntity> : RepositoryBase<TEntity, ApplicationContext> where TEntity : class, new()
    {
        public Repository(ApplicationContext context) : base(context) { }
    }
}
