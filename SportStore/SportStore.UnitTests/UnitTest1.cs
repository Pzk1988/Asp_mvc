using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using System.Collections.Generic;
using SportsStore.WebUI.Controllers;
using System.Linq;
using SportStore.WebUI.Models;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat1"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat1"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat1"},
                new Product {ProductID = 6, Name = "P6", Category = "Cat1"},
                new Product {ProductID = 7, Name = "P7", Category = "Cat1"},
                new Product {ProductID = 8, Name = "P8", Category = "Cat1"},
                new Product {ProductID = 9, Name = "P9", Category = "Cat1"},
            });

            ProductController controller = new ProductController(mock.Object, 3);
            IEnumerable<Product> result = (controller.List(null, 2).Model as ProductListViewModel).Products;

            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 3);
            Assert.AreEqual(prodArray[0].Name, "P4");
            Assert.AreEqual(prodArray[1].Name, "P5");
            Assert.AreEqual(prodArray[2].Name, "P6");
        }

        [TestMethod]
        public void TestMethod2()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat1"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat1"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat1"},
                new Product {ProductID = 6, Name = "P6", Category = "Cat1"},
                new Product {ProductID = 7, Name = "P7", Category = "Cat1"},
                new Product {ProductID = 8, Name = "P8", Category = "Cat1"},
                new Product {ProductID = 9, Name = "P9", Category = "Cat1"},
            });

            ProductController controller = new ProductController(mock.Object, 4);
            IEnumerable<Product> result = (controller.List(null, 3).Model as ProductListViewModel).Products;

            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 1);
            Assert.AreEqual(prodArray[0].Name, "P9");
        }

        [TestMethod]
        public void TestMethod3()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductID = 2, Name = "P2", Category = "Cat1"},
                new Product {ProductID = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductID = 4, Name = "P4", Category = "Cat1"},
                new Product {ProductID = 5, Name = "P5", Category = "Cat1"},
                new Product {ProductID = 6, Name = "P6", Category = "Cat1"},
                new Product {ProductID = 7, Name = "P7", Category = "Cat1"},
                new Product {ProductID = 8, Name = "P8", Category = "Cat1"},
                new Product {ProductID = 9, Name = "P9", Category = "Cat1"},
            });

            ProductController controller = new ProductController(mock.Object, 4);
            IEnumerable<Product> result = (controller.List(null, 1).Model as ProductListViewModel).Products;

            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 4);
            Assert.AreEqual(prodArray[0].Name, "P1");
            Assert.AreEqual(prodArray[1].Name, "P2");
            Assert.AreEqual(prodArray[2].Name, "P3");
            Assert.AreEqual(prodArray[3].Name, "P4");
        }

        [TestMethod]
        public void CategoryFilter()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name="P1", Category = "Cat1"},
                new Product {ProductID = 2, Name="P2", Category = "Cat1"},
                new Product {ProductID = 3, Name="P3", Category = "Cat3"},
                new Product {ProductID = 4, Name="P4", Category = "Cat1"},
                new Product {ProductID = 5, Name="P5", Category = "Cat11"},
                new Product {ProductID = 6, Name="P6", Category = "Cat1"},
                new Product {ProductID = 7, Name="P7", Category = "Cat2"},
                new Product {ProductID = 8, Name="P8", Category = "Cat"},
                new Product {ProductID = 9, Name="P9", Category = "Cat3"},
            });

            ProductController controller = new ProductController(mock.Object, 4);
            IEnumerable<Product> result = (controller.List("Cat1", 1).Model as ProductListViewModel).Products;

            Product[] prodArray = result.ToArray();
            Assert.IsTrue(prodArray.Length == 4);
            Assert.AreEqual(prodArray[0].ProductID, 1);
            Assert.AreEqual(prodArray[0].Name, "P1");

            Assert.AreEqual(prodArray[1].ProductID, 2);
            Assert.AreEqual(prodArray[1].Name, "P2");

            Assert.AreEqual(prodArray[2].ProductID, 4);
            Assert.AreEqual(prodArray[2].Name, "P4");

            Assert.AreEqual(prodArray[3].ProductID, 6);
            Assert.AreEqual(prodArray[3].Name, "P6");
        }

        [TestMethod]
        public void CategoryFilterNoEntries()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name="P1", Category = "Cat1"},
                new Product {ProductID = 2, Name="P2", Category = "Cat1"},
                new Product {ProductID = 3, Name="P3", Category = "Cat3"},
                new Product {ProductID = 4, Name="P4", Category = "Cat1"},
                new Product {ProductID = 5, Name="P5", Category = "Cat11"},
                new Product {ProductID = 6, Name="P6", Category = "Cat1"},
                new Product {ProductID = 7, Name="P7", Category = "Cat2"},
                new Product {ProductID = 8, Name="P8", Category = "Cat"},
                new Product {ProductID = 9, Name="P9", Category = "Cat3"},
            });

            ProductController controller = new ProductController(mock.Object, 3);
            Product[] product = (controller.List("Cat12").Model as ProductListViewModel).Products.ToArray();

            Assert.IsTrue(product.Length == 0);
        
        }

        [TestMethod]
        public void CategoryFilterSecondPage()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name="P1", Category = "Cat1"},
                new Product {ProductID = 2, Name="P2", Category = "Cat1"},
                new Product {ProductID = 3, Name="P3", Category = "Cat3"},
                new Product {ProductID = 4, Name="P4", Category = "Cat1"},
                new Product {ProductID = 5, Name="P5", Category = "Cat11"},
                new Product {ProductID = 6, Name="P6", Category = "Cat1"},
                new Product {ProductID = 7, Name="P7", Category = "Cat2"},
                new Product {ProductID = 8, Name="P8", Category = "Cat"},
                new Product {ProductID = 9, Name="P9", Category = "Cat3"},
            });

            ProductController controller = new ProductController(mock.Object, 3);
            Product[] product = (controller.List("Cat1", 2).Model as ProductListViewModel).Products.ToArray();

            Assert.IsTrue(product.Length == 1);
            Assert.AreEqual(product[0].ProductID, 6);
            Assert.AreEqual(product[0].Name, "P6");
        }

        [TestMethod]
        public void CategoryFilterTwoPages()
        {
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(new Product[]
            {
                new Product {ProductID = 1, Name="P1", Category = "Cat1"},
                new Product {ProductID = 2, Name="P2", Category = "Cat1"},
                new Product {ProductID = 3, Name="P3", Category = "Cat3"},
                new Product {ProductID = 4, Name="P4", Category = "Cat1"},
                new Product {ProductID = 5, Name="P5", Category = "Cat11"},
                new Product {ProductID = 6, Name="P6", Category = "Cat1"},
                new Product {ProductID = 7, Name="P7", Category = "Cat1"},
                new Product {ProductID = 8, Name="P8", Category = "Cat1"},
                new Product {ProductID = 9, Name="P9", Category = "Cat3"},
            });

            ProductController controller = new ProductController(mock.Object, 4);
            PageInfo pageInfo = (controller.List("Cat1", 1).Model as ProductListViewModel).PageInfo;

            Assert.IsTrue(pageInfo.CurrentPage == 1);
            Assert.IsTrue(pageInfo.TotalItems == 6);
            Assert.IsTrue(pageInfo.TotalPages == 2);
        }
    }
}
