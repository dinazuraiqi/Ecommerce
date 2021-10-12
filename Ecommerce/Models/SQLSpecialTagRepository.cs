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
        public async Task<Result> Add(SpecialTag specialTag)
        {
            Result result = new Result();
            try
            {
                SpecialTag specialTagExist = context.SpecialTags.Find(specialTag.Id);
                if(specialTagExist == null)
                {
                    context.SpecialTags.Add(specialTag);
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = specialTag;
                }                
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
           
            return result;
        }

        public async Task<Result> Delete(int Id)
        {
            Result result = new Result();
            try
            {
                SpecialTag specialTag = context.SpecialTags.Find(Id);
                if (specialTag != null)
                {
                    context.SpecialTags.Remove(specialTag);
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = specialTag;
                }
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }           
            return result;
        }

        public IEnumerable<SpecialTag> GetAllSpecialTags()
        {
            return context.SpecialTags;
        }

        public SpecialTag GetSpecialTag(int Id)
        {
            return context.SpecialTags.Find(Id);
        }

        public async Task<Result> Update(SpecialTag specialTagChanges)
        {
            Result result = new Result();
            try
            {
                var specialTag = context.SpecialTags.Attach(specialTagChanges);
                if(specialTag != null)
                {
                    specialTag.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await context.SaveChangesAsync();
                    result.Success = true;
                    result.ResultObject = specialTagChanges;
                }                
            }
            catch(Exception e)
            {
                result.ErrorMessage = e.Message;
            }
            
            return result;
        }
    }
}
