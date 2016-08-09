using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SB.DAL;

namespace SB.App_Start
{
    public class BlogDependsResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public BlogDependsResolver(IKernel kernel)
        {
            _kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            _kernel.Bind<IDataSource>().ToMethod(c => DataContextFactory.Instance.Create());
        }
    }
}