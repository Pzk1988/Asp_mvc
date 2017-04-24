using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.Models;

namespace SportStore.UnitTests
{
    [TestClass]
    public class PagingInfoUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PageInfo pageInfo = new PageInfo { TotalItems = 10, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 4);

            pageInfo = new PageInfo { TotalItems = 6, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 2);

            pageInfo = new PageInfo { TotalItems = 7, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 3);

            pageInfo = new PageInfo { TotalItems = 5, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 2);

            pageInfo = new PageInfo { TotalItems = 3, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 1);

            pageInfo = new PageInfo { TotalItems = 1, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 1);

            pageInfo = new PageInfo { TotalItems = 0, ItemsPerPage = 3 };
            Assert.IsTrue(pageInfo.TotalPages == 0);
        }
    }
}
