using Battleship.BL.Entities.Interface;
using Battleship.BL.Logic.Interface;
using System.Collections.Generic;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Entities.Ships
{
    /// <summary>
    /// Abstract class for Battle ships 
    /// defines minimum contract for any ship type 
    /// </summary>
    public abstract class BaseBattleship : IBattleship
    {
        //Properties

        /// <summary>
        /// List of Points(x,y) occupied on board by a ship 
        /// </summary>
        public IList<ShipPoint> PointsOccupied { get; set; }
        
        /// <summary>
        /// List of Points(x,y) attacked on a ship 
        /// </summary>
        public IList<ShipPoint> PointsAttacked { get; set; }

        /// <summary>
        /// Health of the ship at any give time
        /// Eg. Damaged/ Undamaged/ Sunk
        /// </summary>
        public ShipHealth Health { get; set; }

        /// <summary>
        /// Size(number of points) required by a ship
        /// </summary>
        public abstract int Size { get; }

        //Dependancy
        private readonly IShipOrientationFactory _shipOrientationFactory;

        public BaseBattleship(IShipOrientationFactory shipOrientationFactory)
        {
            _shipOrientationFactory = shipOrientationFactory;
            Health = ShipHealth.Undamaged;
        }

        /// <summary>
        /// Get the Points(X,Y) on board occupied by a ship based on ship Type, Orientation 
        /// and start point location 
        /// </summary>
        /// <param name="startPosition">start point(x,y) for placing ship</param>
        /// <param name="orientation">Eg. North/South/West/East</param>
        /// <returns>List of all the points(x,y) occupied by ship</returns>
        public virtual IList<ShipPoint> GetPointsOccupiedOnBoard(ShipPoint startPosition, ShipOrientation orientation)
        {
            var orientationCalc = _shipOrientationFactory.GetShipPlacementOrientation(orientation);

            return orientationCalc?.GetPointsOccupiedByPlacementDirection(startPosition, Size);
        }
    }
}
