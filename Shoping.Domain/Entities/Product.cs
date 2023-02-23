using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Product : BaseEntity
    {
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Category_Id { get; set; }
        public Category Category { get; set; } = new Category();
        public decimal Price { get; set; }
    }
}
