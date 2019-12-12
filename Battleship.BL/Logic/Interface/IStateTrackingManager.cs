using Battleship.BL.Common;
using Battleship.BL.Models;

namespace Battleship.BL.Logic.Interface
{
    /// <summary>
    /// Interface for state tracking manager
    /// </summary>
    public interface IStateTrackingManager
    {
        /// <summary>
        /// Reset the board and start a new game
        /// </summary>
        void ResetBoard();

        /// <summary>
        /// Place ship on the board 
        /// </summary>
        /// <param name="shipToBePlaced">Detail about the ship like orientation, type, start point location etc</param>
        /// <param name="isSuccessfullyPlaced">Resulting param - showing success/failure in placing ship</param>
        /// <returns>Message detailing outcome of the ship placement task</returns>
        string AddShipToBoard(ShipModel shipToBePlaced, out bool isSuccessfullyPlaced);

        /// <summary>
        /// Method to make an attack on board at a give location
        /// </summary>
        /// <param name="attackingPosition">Point of attack (X and Y details of the point)</param>
        /// <param name="isGameOver">Status of the game after this attack
        /// Check to see if all the placed ship on board are sunk - Then Game is Over 
        /// and board will be reset for new game
        /// </param>
        /// <returns>Attack Status - HIT/MISS/SUNK etc</returns>
        GameEnum.AttackResult AttackOnBoard(ShipPointLocationModel attackingPosition, out bool isGameOver);
    }
}
