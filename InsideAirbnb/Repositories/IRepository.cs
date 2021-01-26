using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace InsideAirbnb.Repositories
{
    public interface IRepository<T>
    {
        public Task<T> Get(int id);
        public IQueryable<T> Filter(Filter filter);
    }
}
