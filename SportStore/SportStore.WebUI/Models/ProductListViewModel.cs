using SportsStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SportStore.WebUI.Models
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products {get; set;}
        public PageInfo PageInfo { get; set; }
        public string Category { get; set; }
    };
}