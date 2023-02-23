using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }= string.Empty;
    }
}