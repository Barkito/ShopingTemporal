using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities.Comon
{
    public class DetailBaseEntity:BaseEntity
    {
        
        public int Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Quality { get; set; }
        public decimal Total { get; set; }
        
    }
}