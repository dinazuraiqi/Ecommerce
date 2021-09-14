using Ecommerce.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Models
{
    public class SQLSpecialTagRepository : ISpecialTagRepository
    {
        private readonly ApplicationDbContext context;

        public SQLSpecialTagRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public SpecialTag Add(SpecialTag specialTag)
        {
            context.SpecialTags.Add(specialTag);
            context.SaveChanges();
            return specialTag;
        }

        public SpecialTag Delete(int Id)
        {
            SpecialTag specialTag = context.SpecialTags.Find(Id);
            if (specialTag != null)
            {
                context.SpecialTags.Remove(specialTag);
                context.SaveChanges();
            }
            return specialTag;
        }

        public IEnumerable<SpecialTag> GetAllSpecialTags()
        {
            return context.SpecialTags;
        }

        public SpecialTag GetSpecialTag(int Id)
        {
            return context.SpecialTags.Find(Id);
        }

        public SpecialTag Update(SpecialTag specialTagChanges)
        {
            var specialTag = context.SpecialTags.Attach(specialTagChanges);
            specialTag.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return specialTagChanges;
        }
    }
}
