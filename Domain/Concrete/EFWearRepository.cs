using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFWearRepository : IWearRepository
    {
        EFDbContext context = new EFDbContext();
        public IEnumerable<Wear> Wears
        {
            get { return context.Wears; }
        }

        public Wear DeleteWear(int wearId)
        {
            Wear dbEntry = context.Wears.Find(wearId);
            if (dbEntry != null)
            {
                context.Wears.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }


        public void SaveWear(Wear wear)
        {
            if (wear.WearId == 0)
            {
                context.Wears.Add(wear);
            }
            else
            {
                Wear dbEntry = context.Wears.Find(wear.WearId);
                if (dbEntry != null)
                {
                    dbEntry.Name = wear.Name;
                    dbEntry.Fabric = wear.Fabric;
                    dbEntry.Description = wear.Description;
                    dbEntry.Category = wear.Category;
                    dbEntry.Price = wear.Price;
                    dbEntry.Size = wear.Size;
                    dbEntry.ImageData = wear.ImageData;
                    dbEntry.ImageMimeType = wear.ImageMimeType;
                }
            }
            context.SaveChanges();
        }
    }
}
