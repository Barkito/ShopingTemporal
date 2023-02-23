using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Purchase : BaseEntity
    {
        public string Number { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int Provider_Id { get; set; }
        public Provider Provider { get; set; } = new Provider();
        public int Created_By { get; set; }
        public User User { get; set; } = new User();
        public DateTime Created_At { get; set; }
        public int Modified_By { get; set; }
        public DateTime Modified_At { get; set; }

         public ICollection<PurchaseDetail> PurchaseDetails { get; set; } = new  List<PurchaseDetail>();
    }
}
