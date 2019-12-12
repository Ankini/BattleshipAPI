using Battleship.BL.Entities.Interface;
using Battleship.BL.Entities.Orientations;
using Battleship.BL.Entities.Ships;
using Battleship.BL.Logic;
using Battleship.BL.Logic.Interface;
using Unity;
using Unity.Lifetime;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Config
{
    /// <summary>
    /// Registr of Dependancies
    /// </summary>
    public static class CompositionRoot
    {
        public static void RegisterServices<T>(IUnityContainer container) where T : LifetimeManager, new()
        {
            RegisterEntities(container);
        }

        private static void RegisterEntities(IUnityContainer container)
        {
            container.RegisterType<IStateTrackingManager, StateTrackingManager>(new PerResolveLifetimeManager());

            container.RegisterType<IBattleShipFactory, BattleShipFactory>();

            container.RegisterType<IBattleship, AircraftCarrierShip>(ShipType.AircraftCarrier.ToString());
            container.RegisterType<IBattleship, SubmarineShip>(ShipType.Submarine.ToString());
            container.RegisterType<IBattleship, Battleship4>(ShipType.Battleship.ToString());
            container.RegisterType<IBattleship, CruiserShip>(ShipType.Cruiser.ToString());
            container.RegisterType<IBattleship, DestroyerShip>(ShipType.Destroyer.ToString());


            container.RegisterType<IShipOrientationFactory, ShipOrientationFactory>();

            container.RegisterType<IShipOrientation, NorthOrientation>(ShipOrientation.North.ToString());
            container.RegisterType<IShipOrientation, EastOrientation>(ShipOrientation.East.ToString());
            container.RegisterType<IShipOrientation, SouthOrientation>(ShipOrientation.South.ToString());
            container.RegisterType<IShipOrientation, WestOrientation>(ShipOrientation.West.ToString());
        }
    }
}
