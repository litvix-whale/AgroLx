using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities;
public class ProductImage:EntityBase<int>
{
    // save url in bd and use cloud for images (maybe better saving decoded images in db)
    public string Url { get; set; } = null!;
    
    // user can choice the priority for photo (for ux will be better use drag and drop and using arrows on keyboard)
    public int UiPriority { get; set; }
    
    // many images for one product
    public int ProductId { get; set; }
    public virtual Product Product { get; set; } = null!;
}
