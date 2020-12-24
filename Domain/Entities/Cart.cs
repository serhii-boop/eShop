using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines { get { return lineCollection; } }

        public void AddItem(Wear wear, int quantity)
        {
            CartLine line = lineCollection
                .Where(b => b.Wear.WearId == wear.WearId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Wear = wear, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Wear wear)
        {
            lineCollection.RemoveAll(l => l.Wear.WearId == wear.WearId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Wear.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }

    public class CartLine
    {
        public Wear Wear { get; set; }
        public int Quantity { get; set; }
    }
}
