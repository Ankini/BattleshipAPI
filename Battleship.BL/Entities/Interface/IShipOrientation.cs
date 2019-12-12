using System.Collections.Generic;

namespace Battleship.BL.Entities.Interface
{
    public interface IShipOrientation
    {
        IList<ShipPoint> GetPointsOccupiedByPlacementDirection(ShipPoint startPosition, int shipLength);
    }
}
