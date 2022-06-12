using Kolokwium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium.Services
{
    public class DbService :IDbService
    {
        private readonly MainDbContext _dbContext;

        public DbService(MainDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
