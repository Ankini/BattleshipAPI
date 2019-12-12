using Battleship.BL.Common;
using Battleship.BL.Logic.Interface;
using System.Web;
using Unity;

namespace Battleship.BL.Logic
{
    /// <summary>
    /// Dependancy resolved service 
    /// </summary>
    public class ServiceLocator : IServiceLocator
    {
        public IUnityContainer Container
        {
            get
            {
                return HttpContext.Current.Application[GameConstant.ContainerKey] as IUnityContainer ?? new UnityContainer();
            }
        }

        public T Resolve<T>(string name) => (T)Container.Resolve(typeof(T), name);
    }
}
