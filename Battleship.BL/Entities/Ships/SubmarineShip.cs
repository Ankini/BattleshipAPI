using Battleship.BL.Logic.Interface;

namespace Battleship.BL.Entities.Ships
{
    /// <summary>
    /// Submarine - Occupies 3 points on board
    /// </summary>
    public class SubmarineShip : BaseBattleship
    {
        //Property
        public override int Size { get { return 3; } }

        public SubmarineShip(IShipOrientationFactory shipOrientationFactory) : base(shipOrientationFactory)
        {
        }
    }
}