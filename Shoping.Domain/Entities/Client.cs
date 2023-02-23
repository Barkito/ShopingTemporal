using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Client:BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Ruc { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }
}
