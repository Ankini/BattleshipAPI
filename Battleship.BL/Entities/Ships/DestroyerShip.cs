using Battleship.BL.Logic.Interface;

namespace Battleship.BL.Entities.Ships
{
    /// <summary>
    /// Destroyer - Occupies 2 points on board
    /// </summary>
    public class DestroyerShip : BaseBattleship
    {
        //Property
        public override int Size { get { return 2; } }

        public DestroyerShip(IShipOrientationFactory shipOrientationFactory) : base(shipOrientationFactory) { }
    }
}