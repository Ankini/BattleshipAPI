using Battleship.BL.Entities.Interface;
using System.Collections.Generic;

namespace Battleship.BL.Entities.Orientations
{
    /// <summary>
    /// Depicts that ship will occupy points on EAST from the start point location
    /// </summary>
    public class EastOrientation : IShipOrientation
    {
        public IList<ShipPoint> GetPointsOccupiedByPlacementDirection(ShipPoint startPosition, int shipLength)
        {
            var pointList = new List<ShipPoint>(shipLength);
            for (int i = 0; i < shipLength; i++)
            {
                pointList.Add(new ShipPoint() { XCoordinate = startPosition.XCoordinate + i, YCoordinate = startPosition.YCoordinate });
            }
            return pointList;
        }
    }
}
