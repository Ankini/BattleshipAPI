using System.Collections.Generic;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Entities.Interface
{
    /// <summary>
    /// Interface - Bare Min Contract for any type of battleship
    /// </summary>
    public interface IBattleship
    {    /// <summary>
         /// List of Points(x,y) occupied on board by a ship 
         /// </summary>
        IList<ShipPoint> PointsOccupied { get; set; }

        /// <summary>
        /// List of Points(x,y) attacked on a ship 
        /// </summary>
        IList<ShipPoint> PointsAttacked { get; set; }

        /// <summary>
        /// Health of the ship at any give time
        /// Eg. Damaged/ Undamaged/ Sunk
        /// </summary>
        ShipHealth Health { get; set; }

        /// <summary>
        /// Size(number of points) required by a ship
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Get the Points(X,Y) on board occupied by a ship based on ship Type, Orientation 
        /// and start point location 
        /// </summary>
        /// <param name="startPosition">start point(x,y) for placing ship</param>
        /// <param name="orientation">Eg. North/South/West/East</param>
        /// <returns>List of all the points(x,y) occupied by ship</returns>
        IList<ShipPoint> GetPointsOccupiedOnBoard(ShipPoint startPosition, ShipOrientation orientation);
    }
}
