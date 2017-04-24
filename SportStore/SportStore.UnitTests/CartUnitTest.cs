using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.Domain.Entities;
using System.Linq;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class CartUnitTest
    {
        [TestMethod]
        public void CanAddNewLines()
        {
            Cart cart = new Cart();
            Assert.IsTrue(cart.Count() == 0);

            cart.AddItem(new Product { ProductID = 1, Name = "P1" }, 1);
            cart.AddItem(new Product { ProductID = 2, Name = "P2" }, 1);
            Assert.IsTrue(cart.Count() == 2);

            CartLine[] result = cart.Lines().ToArray();
            Assert.AreEqual(result[0].Product, new Product { ProductID = 1, Name = "P1" });
            Assert.IsTrue(result[0].Quantity == 1);
            Assert.AreEqual(result[1].Product, new Product { ProductID = 2, Name = "P2" });
            Assert.IsTrue(result[1].Quantity == 1);
        }

        [TestMethod]
        public void CanAddQuantityForExistingLine()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);

            CartLine[] cartLines = cart.Lines().ToArray();

            Assert.IsTrue(cartLines[0].Quantity == 11);
            Assert.IsTrue(cartLines[1].Quantity == 1);
            Assert.IsTrue(cart.Count() == 2);
        }

        [TestMethod]
        public void CanRemoveLines()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1" };
            Product p2 = new Product { ProductID = 2, Name = "P2" };

            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 10);
            CartLine[] cartLines = cart.Lines().ToArray();

            Assert.IsTrue(cartLines[0].Quantity == 11);
            Assert.IsTrue(cartLines[1].Quantity == 1);
            Assert.IsTrue(cart.Count() == 2);

            cart.RemoveItem(p1, 11);
            cartLines = cart.Lines().ToArray();

            Assert.IsTrue(cartLines[0].Quantity == 1);
            Assert.IsTrue(cart.Count() == 1);
        }

        [TestMethod]
        public void CalculateTotal()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 20.00m };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 201.00m };

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);

            Assert.IsTrue(cart.ComputeTotalValue() == 241.00m);
        }

        [TestMethod]
        public void Clear()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 20.00m };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 201.00m };

            cart.AddItem(p1, 2);
            cart.AddItem(p2, 1);
            Assert.IsTrue(cart.Count() == 2);

            cart.Clear();
            Assert.IsTrue(cart.ComputeTotalValue() == 0m);
            Assert.IsTrue(cart.Count() == 0);
        }

        [TestMethod]
        public void ClearNotAll()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 20.00m };

            cart.AddItem(p1, 10);
            cart.RemoveItem(p1, 1);

            CartLine[] cartLineList = cart.Lines().ToArray();
            Assert.IsTrue(cartLineList[0].Quantity == 9);

            cart.RemoveItem(p1, 2);
            Assert.IsTrue(cartLineList[0].Quantity == 7);
        }

        [TestMethod]
        public void ClearAll()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 20.00m };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 20.00m };

            cart.AddItem(p1, 10);
            cart.AddItem(p2, 5);
            cart.RemoveItem(p1, 10);         
            Assert.IsTrue(cart.Count() == 1);
            Assert.AreEqual(cart.Lines().ToArray()[0].Product.Name, "P2");
        }

        [TestMethod]
        public void ClearMoreThanAll()
        {
            Cart cart = new Cart();
            Product p1 = new Product { ProductID = 1, Name = "P1", Price = 20.00m };
            Product p2 = new Product { ProductID = 2, Name = "P2", Price = 20.00m };

            cart.AddItem(p1, 10);
            cart.AddItem(p2, 5);
            cart.RemoveItem(p1, 12);

            Assert.IsTrue(cart.Count() == 1);
            Assert.AreEqual(cart.Lines().ToArray()[0].Product.Name, "P2");
        }
    }
}
