using System;
using System.Threading;
using DataLayer.Context;
using ServiceLayer.Interfaces;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Web;

namespace IocConfig
{
    public static class SampleObjectFactory
    {
        private static readonly Lazy<Container> ContainerBuilder =
            new Lazy<Container>(DefaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

        public static IContainer Container
        {
            get { return ContainerBuilder.Value; }
        }

        private static Container DefaultContainer()
        {
            return new Container(x =>
            {
                x.For<IUnitOfWork>().HybridHttpOrThreadLocalScoped().Use(() => new ShopDbContext());

                x.Scan(scan =>
                {
                    scan.AssemblyContainingType<IUserService>();
                    scan.TheCallingAssembly();
                    scan.WithDefaultConventions();
                });
            });
        }
    }
}
