using Battleship.BL.Entities.Interface;
using Battleship.BL.Logic.Interface;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Logic
{
    /// <summary>
    /// Factory listing all the posible orientation(North/ South/ West/ East) for ship placement 
    /// </summary>
    public class ShipOrientationFactory : IShipOrientationFactory
    {
        //Dependancy 
        //Service resolver from Unity container
        private readonly IServiceLocator _serviceLocator;


        //Constructor -DI
        public ShipOrientationFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IShipOrientation GetShipPlacementOrientation(ShipOrientation orientation)
        {
            return _serviceLocator.Resolve<IShipOrientation>(orientation.ToString());
        }
    }
}