using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository repository;
        private int pageSize;

        public ProductController(IProductRepository productRepository, int size = 4)
        {
            pageSize = size;
            repository = productRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ViewResult List(string category, int page = 1)
        {
            IEnumerable<Product> products = repository.Products
                    .Where(p => p.Category == category || category == null)
                    .OrderBy(p => p.Name)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize);

            PageInfo pageInfo = new PageInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = category == null ? 
                    repository.Products.Count() : 
                    repository.Products.Where(p => p.Category == category).Count()
            };

            ProductListViewModel viewModel = new ProductListViewModel()
            {
                Products = products,
                PageInfo = pageInfo,               
                Category = category,
        };
            return View(viewModel);
        }
    }
}