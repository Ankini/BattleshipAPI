using Battleship.BL.Logic.Interface;

namespace Battleship.BL.Entities.Ships
{
    /// <summary>
    /// Cruiser - Occupies 3 points on board
    /// </summary>
    public class CruiserShip : BaseBattleship
    {
        //Property
        public override int Size { get { return 3; } }

        public CruiserShip(IShipOrientationFactory shipOrientationFactory) : base(shipOrientationFactory)
        {
        }
    }
}