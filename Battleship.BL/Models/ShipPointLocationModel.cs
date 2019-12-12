using Battleship.BL.Common;
using System.ComponentModel.DataAnnotations;

namespace Battleship.BL.Models
{
    /// <summary>
    /// Point location in terms of X and Y aixs on board 
    /// requested for attack
    /// </summary>
    public class ShipPointLocationModel
    {
        /// <summary>
        /// X-axis 
        /// should be between 1 and 10
        /// </summary>
        [Required]
        [Range(1, GameConstant.MaxXOnBoard, ErrorMessage = "X position is out of the board! Please refere the correct board size.")]
        public int? XCoordinate { get; set; }

        /// <summary>
        /// Y-axis 
        /// should be between 1 and 10
        /// </summary>
        [Required]
        [Range(1, GameConstant.MaxYOnBoard, ErrorMessage = "Y position is out of the board! Please refere the correct board size.")]
        public int? YCoordinate { get; set; }
    }
}
