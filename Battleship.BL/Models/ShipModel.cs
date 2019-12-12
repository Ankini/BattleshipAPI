using System.ComponentModel.DataAnnotations;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Models
{
    /// <summary>
    /// Input model for placing a ship on the board 
    /// </summary>
    public class ShipModel
    {
        /// <summary>
        /// Type of the ship (AircraftCarrier/ Battleship/ Submarine/ Cruiser/ Destroyer) 
        /// requested to be placed on board
        /// </summary>
        [Required]
        public ShipType Ship { get; set; }

        /// <summary>
        /// X and Y cordinate for starting point for placing the ship
        /// </summary>
        [Required]
        public ShipPointLocationModel StartPoint { get; set; }

        /// <summary>
        /// Ship Orientation
        /// Posible values - North/ South/ East/ West
        /// </summary>
        [Required]
        public ShipOrientation Orientation { get; set; }
    }
}
