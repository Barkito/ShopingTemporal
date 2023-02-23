using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shoping.Domain.Entities.Comon;

namespace Shoping.Domain.Entities
{
    public class PurchaseDetail : DetailBaseEntity
    {

        public int Purchase_Id { get; set; }
        public Purchase Purchase { get; set; } = new Purchase();
    }
}