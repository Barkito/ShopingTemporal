using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shoping.Domain.Entities
{
    public class Provider : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string RUC { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Direction { get; set; } = string.Empty;
    }
}
