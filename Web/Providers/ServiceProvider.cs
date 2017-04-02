using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;

namespace Web.Providers
{
    public class ServiceProvider
    {
        private static ServiceProvider _instance;

        private static IContainer _container;

        public static void SetContainer(IContainer container)
        {
            _container = container;
        }

        private ServiceProvider()
        {
        }

        public bool TryResolve(Type type, out object value) => _container.TryResolve(type, out value);

        public TType GetService<TType>()
        {
            return _container.Resolve<TType>();
        }

        public static ServiceProvider Instance => _instance ?? (_instance = new ServiceProvider());
    }
}