using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Category:EntityBase<int>
    {
        public string Title { get; set; } = null!;
        
        // one parent category may have many children categories
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        
        // many children categories for one parent category
        public virtual ICollection<Category> Children { get; set; } = [];

        // many products for one category
        public virtual ICollection<Product> Products { get; set; } = [];
    }
}
