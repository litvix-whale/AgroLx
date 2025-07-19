using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Cart:EntityBase<Guid>
    {
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
