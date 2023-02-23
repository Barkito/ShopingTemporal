using Shoping.Application.Common.Interfaces.Repositories;
using Shoping.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Persistence.Repositories
{
    public class DeliveriesDetailsRepository :GenericRepository<DeliveryDetail> , IDeliveryDetail
    {
        public DeliveriesDetailsRepository(AppDbContext dbContext) : base(dbContext) 
        {
            
        }
       
    }
}
