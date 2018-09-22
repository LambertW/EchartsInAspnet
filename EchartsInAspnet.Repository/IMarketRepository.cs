using EchartsInAspnet.Entities;
using SmartSql.DyRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchartsInAspnet.Repository
{
    public interface IMarketRepository : IRepository<Market, int>
    {
    }
}
