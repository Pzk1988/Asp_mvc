using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Cart
    {
        private List<CartLine> lineCollection;

        public Cart()
        {
            lineCollection = new List<CartLine>();
        }

        public void AddItem(Product product, int quantity)
        {
            CartLine cartLine = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if(cartLine == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                cartLine.Quantity += quantity;
            }
        }

        public void RemoveItem(Product product, int quantity)
        {
            CartLine cartLine = lineCollection.FirstOrDefault(c => c.Product.ProductID == product.ProductID);
            if(cartLine != null)
            {
                if(cartLine.Quantity <= quantity)
                {
                    lineCollection.Remove(cartLine);
                }
                else
                {
                    cartLine.Quantity -= quantity;
                }
            }
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines()
        {
            return lineCollection;
        }

        public int Count()
        {
            return lineCollection.Count();
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(p => p.Product.Price * p.Quantity);
        }
    }
}
