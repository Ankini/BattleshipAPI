using Battleship.BL.Entities.Interface;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Logic.Interface
{
    public interface IShipOrientationFactory
    {
        IShipOrientation GetShipPlacementOrientation(ShipOrientation orientation);
    }
}
