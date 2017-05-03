using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Entities;
using Moq;
using SportStore.Domain.Repository.Database;
using System.Configuration;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //Mock<IProductRepository> mock = new Mock<IProductRepository>();
            //mock.Setup(m => m.Products).Returns(new List<Product>
            //{
            //    new Product { Name = "Football", Price = 25 },
            //    new Product { Name = "Surf board", Price = 179 },
            //    new Product { Name = "Running shoes", Price = 95 }
            //});

            //kernel.Bind<IProductRepository>().ToConstant(mock.Object);

            EmailSettings settings = new EmailSettings{ WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")};

            kernel.Bind<IProductRepository>().To<DatabaseRepository>();
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings", settings);
        }
    }
}