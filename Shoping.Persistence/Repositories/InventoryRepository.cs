using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shoping.Application.Common.Interfaces.Repositories;

namespace Shoping.Persistence.Repositories
{
    public class InventoryRepository : GenericRepository<Inventary> , IInventaryRepository
    {
        public InventoryRepository(AppDbContext dbContext) : base(dbContext) 
        {

        }    
    }
}
