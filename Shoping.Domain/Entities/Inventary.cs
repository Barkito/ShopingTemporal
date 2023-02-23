using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Inventary : BaseEntity
    {
        public int Price { get; set; }
        public int Stock { get; set; }
        public int Product_Id { get; set; }
        public Product products { get; set; } = new Product();
    }
}
