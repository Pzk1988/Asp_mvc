using SportsStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities;
using System.Data.Entity;

namespace SportStore.Domain.Repository.Database
{
    public class DatabaseRepository : IProductRepository
    {
        public IEnumerable<Product> Products
        {
            get
            {
                using (CS_DatabaseEntities context = new CS_DatabaseEntities())
                {
                    return context.Products.Select(p => new Product { Category = p.Category, Description = p.Description, Name = p.Name, Price = p.Price, ProductID = p.ProductID }).ToList();
                }
             } 
        }
    }
}
