using Battleship.BL.Logic.Interface;

namespace Battleship.BL.Entities.Ships
{
    /// <summary>
    /// Battleship - Occupies 4 points on board
    /// </summary>
    public class Battleship4 : BaseBattleship
    {
        //Property
        public override int Size { get { return 4; } }

        //Constructor
        public Battleship4(IShipOrientationFactory shipOrientationFactory) : base(shipOrientationFactory)
        {
        }
    }
}