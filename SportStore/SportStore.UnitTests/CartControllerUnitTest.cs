using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Linq;
using SportsStore.WebUI.Controllers;
using System.Web.Mvc;
using SportsStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartControllerUnitTest
    {
        [TestMethod]
        public void AddToCart()
        {
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },
            }.AsQueryable());

            CartController controller = new CartController(repository.Object, new EmailOrderProcessor(new EmailSettings { WriteAsFile = true }));

            Cart cart = new Cart();

            controller.AddToCart(cart, 1, null);
            Assert.AreEqual(cart.Count(), 1);
            Assert.AreEqual(cart.Lines().ToArray()[0].Product.ProductID, 1);
        }

        [TestMethod]
        public void AddingProductToCartGoesToCartScreen()
        {
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },
            }.AsQueryable());

            CartController controller = new CartController(repository.Object, new EmailOrderProcessor(new EmailSettings { WriteAsFile = true }));
            Cart cart = new Cart();
            RedirectToRouteResult result = controller.AddToCart(cart, 1, "/My/url");
            Assert.AreEqual(result.RouteValues["action"], "Index");
            Assert.AreEqual(result.RouteValues["returnUrl"], "/My/url");
        }

        [TestMethod]
        public void CanViewCartContent()
        {
            Mock<IProductRepository> repository = new Mock<IProductRepository>();
            repository.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Apples" },
            }.AsQueryable());

            Cart cart = new Cart();
            cart.AddItem(repository.Object.Products.FirstOrDefault(), 2);
            CartController controller = new CartController(repository.Object, new EmailOrderProcessor(new EmailSettings { WriteAsFile = true }));
            CartViewModel viewModel = controller.Index(cart, "/Home/Url").ViewData.Model as CartViewModel;

            Assert.AreEqual(viewModel.returnUrl, "/Home/Url");
            Assert.AreEqual(viewModel.cart, cart);
        }

        [TestMethod]
        public void CannotCheckoutEmptyCart()
        {
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            CartController controller = new CartController(null, orderProcessor.Object);
            Cart cart = new Cart();
            ShippingDetails shippingDetails = new ShippingDetails();

            ViewResult result = controller.Checkout(cart, shippingDetails) as ViewResult;
            orderProcessor.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CannotCheckoutInvalidCart()
        {
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            CartController controller = new CartController(null, orderProcessor.Object);
            Cart cart = new Cart();
            cart.AddItem(new Product { ProductID = 1, Name = "P1" }, 1);
            ShippingDetails shippingDetails = new ShippingDetails();

            controller.ModelState.AddModelError("Error", "Error");
            ViewResult result = controller.Checkout(cart, shippingDetails) as ViewResult;
            orderProcessor.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Never());
            Assert.AreEqual("", result.ViewName);
            Assert.AreEqual(false, result.ViewData.ModelState.IsValid);
        }

        [TestMethod]
        public void CanCheckoutCart()
        {
            Mock<IOrderProcessor> orderProcessor = new Mock<IOrderProcessor>();
            CartController controller = new CartController(null, orderProcessor.Object);
            Cart cart = new Cart();
            cart.AddItem(new Product { ProductID = 1, Name = "P1" }, 1);
            ShippingDetails shippingDetails = new ShippingDetails();
            
            ViewResult result = controller.Checkout(cart, shippingDetails) as ViewResult;
            orderProcessor.Verify(m => m.ProcessOrder(It.IsAny<Cart>(), It.IsAny<ShippingDetails>()), Times.Once());
            Assert.AreEqual("Completed", result.ViewName);
            Assert.AreEqual(true, result.ViewData.ModelState.IsValid);
        }
    }
}
