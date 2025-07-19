using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Product: EntityBase<int>
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public virtual ICollection<ProductImage> Images { get; set; } = [];
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
    }
}
