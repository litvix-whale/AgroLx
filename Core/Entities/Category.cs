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
        public int? ParentId { get; set; }
        public Category? Parent { get; set; }
        public List<Category> Children { get; set; } = [];
        public List<Product> Products { get; set; } = [];
    }
}
