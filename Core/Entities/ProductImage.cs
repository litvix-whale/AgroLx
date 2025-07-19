using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductImage:EntityBase<int>
    {
        public string Url { get; set; } = null!;
        public int UiPriority { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;
    }
}
