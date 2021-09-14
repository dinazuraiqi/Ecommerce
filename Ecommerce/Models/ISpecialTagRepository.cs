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
        SpecialTag Add(SpecialTag specialTag);
        SpecialTag Update(SpecialTag specialTagChanges);
        SpecialTag Delete(int Id);
    }
}
