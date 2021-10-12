using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public interface ISpecialTagRepository
    {
        SpecialTag GetSpecialTag(int Id);
        IEnumerable<SpecialTag> GetAllSpecialTags();
        Task<Result> Add(SpecialTag specialTag);
        Task<Result> Update(SpecialTag specialTagChanges);
        Task<Result> Delete(int Id);
    }
}
