using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Delivery : BaseEntity
    {
        public int UserilId { get; set; } = default!;
        //public ICollection<User> IdUser { get; set; } = new List<User>();

        public int DeliveryDetailId { get; set; } = default!;
        public DeliveryDetail DeliveryDetail { get; set;} = new DeliveryDetail();
        //public ICollection<Sale> SaleId { get; set;}
        public int SalelId { get; set; } = default!;
    }
}
