using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SportsStore.WebUI.HtmlHelpers;
using System.Web.Mvc;
using SportStore.WebUI.Models;

namespace SportStore.UnitTests
{
    [TestClass]
    public class PageInfoHelperUnitTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            PageInfo pageInfo = new PageInfo { CurrentPage = 2, ItemsPerPage = 3, TotalItems = 21 };
            Func<int, string> pageDelegate = i => "Page" + i;

            HtmlHelper myHelper = null;
            MvcHtmlString result = myHelper.PageLinks(pageInfo, pageDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                          + @"<a class=""btn btn-primary selected"" href=""Page2"">2</a>"
                          + @"<a class=""btn btn-default"" href=""Page3"">3</a>"
                          + @"<a class=""btn btn-default"" href=""Page4"">4</a>"
                          + @"<a class=""btn btn-default"" href=""Page5"">5</a>"
                          + @"<a class=""btn btn-default"" href=""Page6"">6</a>"
                          + @"<a class=""btn btn-default"" href=""Page7"">7</a>",
                          result.ToString());
        }
        [TestMethod]
        public void TestMethod2()
        {
            PageInfo pageInfo = new PageInfo { CurrentPage = 2, ItemsPerPage = 3, TotalItems = 21 };
            Func<int, string> pageDelegate = i => "Page/" + i;

            HtmlHelper myHelper = null;
            MvcHtmlString result = myHelper.PageLinks(pageInfo, pageDelegate);

            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page/1"">1</a>"
                          + @"<a class=""btn btn-primary selected"" href=""Page/2"">2</a>"
                          + @"<a class=""btn btn-default"" href=""Page/3"">3</a>"
                          + @"<a class=""btn btn-default"" href=""Page/4"">4</a>"
                          + @"<a class=""btn btn-default"" href=""Page/5"">5</a>"
                          + @"<a class=""btn btn-default"" href=""Page/6"">6</a>"
                          + @"<a class=""btn btn-default"" href=""Page/7"">7</a>",
                          result.ToString());
        }
    }
}
