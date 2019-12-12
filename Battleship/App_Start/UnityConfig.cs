using Battleship.BL.Common;
using Battleship.BL.Config;
using Battleship.BL.Logic;
using Battleship.BL.Logic.Interface;
using System.Web;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Battleship.Services.Config
{
    /// <summary>
    /// Battleship - Dependacy Registry
    /// </summary>
    public static class UnityConfig
    {
        public static IUnityContainer Container { get; set; }

        public static void RegisterComponents(HttpConfiguration config)
        {
            Container = new UnityContainer();

            CompositionRoot.RegisterServices<HierarchicalLifetimeManager>(Container);
            Container.RegisterInstance<IServiceLocator>(new ServiceLocator());
            HttpContext.Current.Application[GameConstant.ContainerKey] = Container;
            config.DependencyResolver = new UnityDependencyResolver(Container);
        }
    }
}