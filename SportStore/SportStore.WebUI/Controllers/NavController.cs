using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductRepository productRepository;

        public  NavController(IProductRepository repository)
        {
            productRepository = repository;
        }

        public PartialViewResult Menu(string category = null)
        {
            IEnumerable<string> categories = productRepository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x);

            NavViewModel viewModel = new NavViewModel
            {
                Categories = categories,
                SelectedCategory = category
            };

            return PartialView(viewModel);
        }
    }
}