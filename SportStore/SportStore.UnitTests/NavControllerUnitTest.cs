using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using SportsStore.WebUI.Controllers;
using System.Collections.Generic;
using System.Linq;
using SportsStore.WebUI.Models;

namespace SportStore.UnitTests
{
    [TestClass]
    public class NavControllerUnitTest
    {
        [TestMethod]
        public void Category()
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

            NavController controller = new NavController(mock.Object);
            string[] result = (controller.Menu().Model as NavViewModel).Categories.ToArray();

            Assert.IsTrue(result.Length == 5);
            Assert.AreEqual(result[0], "Cat");
            Assert.AreEqual(result[1], "Cat1");
            Assert.AreEqual(result[2], "Cat11");
            Assert.AreEqual(result[3], "Cat2");
            Assert.AreEqual(result[4], "Cat3");
        }


        [TestMethod]
        public void SelectedCategory()
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

            NavController controller = new NavController(mock.Object);
            NavViewModel result = controller.Menu("Cat1").Model as NavViewModel;
            Assert.AreEqual(result.SelectedCategory, "Cat1");

            result = controller.Menu("Cat3").Model as NavViewModel;
            Assert.AreEqual(result.SelectedCategory, "Cat3");

            result = controller.Menu("Cat11").Model as NavViewModel;
            Assert.AreEqual(result.SelectedCategory, "Cat11");
        }
    }
}
