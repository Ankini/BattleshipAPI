using Battleship.BL.Logic.Interface;

namespace Battleship.BL.Entities.Ships
{
    /// <summary>
    /// AircraftCarrier - Occupies 5 points on board
    /// </summary>
    public class AircraftCarrierShip : BaseBattleship
    {
        //Property
        public override int Size { get { return 5; } }

        public AircraftCarrierShip(IShipOrientationFactory shipOrientationFactory) : base(shipOrientationFactory)
        {
        }
    }
}