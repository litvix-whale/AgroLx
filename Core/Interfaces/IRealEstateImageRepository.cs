using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IRealEstateImageRepository : IRepository<ProductImage>
    {
        Task<IEnumerable<ProductImage>> GetImagesByRealEstateIdAsync(Guid realEstateId);
        Task<ProductImage?> GetByRealEstateIdAndPriorityAsync(Guid realEstateId, int priority);
        Task<int> GetMaxPriorityAsync(Guid realEstateId);
        Task DeleteByRealEstateIdAsync(Guid realEstateId);
    }
}
