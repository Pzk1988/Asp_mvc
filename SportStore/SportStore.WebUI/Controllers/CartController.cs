using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Web.Mvc;
using System.Linq;
using SportsStore.WebUI.Models;
using System.Diagnostics;

namespace SportsStore.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        private IOrderProcessor orderProcess;

        public CartController(IProductRepository productRepository, IOrderProcessor orderProcess)
        {
            repository = productRepository;
            this.orderProcess = orderProcess;
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl = returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveItem(product, 1);
            }

            return RedirectToAction("Index", new { returnUrl });
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            CartViewModel viewModel = new CartViewModel()
            {
                cart = cart,
                returnUrl = returnUrl
            };
            return View(viewModel);
        }

        public PartialViewResult Summary(Cart cart)
        {
            Debug.WriteLine(Request.Url.PathAndQuery);
            return PartialView(cart);
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ActionResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty");
            }

            if (ModelState.IsValid == true)
            {
                orderProcess.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }

        }
    }
}