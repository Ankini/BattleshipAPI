using Battleship.BL.Entities.Interface;
using Battleship.BL.Logic.Interface;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Logic
{
    /// <summary>
    /// Factory listing all the posible ships for game
    /// Sample ship types (AircraftCarrier/ Battleship/ Submarine/ Cruiser/ Destroyer) 
    /// </summary>
    public class BattleShipFactory : IBattleShipFactory
    {
        //Dependancy 
        private readonly IServiceLocator _serviceLocator;

        //Constructor - DI
        public BattleShipFactory(IServiceLocator serviceLocator)
        {
            _serviceLocator = serviceLocator;
        }

        public IBattleship GetBattleship(ShipType shipType)
        {
            return _serviceLocator.Resolve<IBattleship>(shipType.ToString());
        }
    }
}