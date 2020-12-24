using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IWearRepository
    {
        IEnumerable<Wear> Wears { get; }
        void SaveWear(Wear wear);
        Wear DeleteWear(int wearId);
    }
}
