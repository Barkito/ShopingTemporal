using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class DeliveryDetail : BaseEntity
    {
        public int Price { get; set; }
        public int Quality { get; set; }
    }
}
