using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Web.Mvc;
using System.Linq;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;

        public CartController(IProductRepository productRepository)
        {
            repository = productRepository;
        }

        public ActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if(product != null)
            {
                GetCart().AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                GetCart().RemoveItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(string returnUrl)
        {
            CartViewModel viewModel = new CartViewModel()
            {
                cart = GetCart(),
                returnUrl = returnUrl
            };
            return View(viewModel);
        }

        private Cart GetCart()
        {
            if(Session["Cart"] == null)
            {
                Cart cart = new Cart();
                Session["Cart"] = cart;
            }
            return (Cart)Session["Cart"];
        }
    }
}