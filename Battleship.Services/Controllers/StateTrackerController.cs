using Battleship.BL.Common;
using Battleship.BL.Common.Extension;
using Battleship.BL.Logic.Interface;
using Battleship.BL.Models;
using System.Web.Http;

namespace Battleship.Services.Controllers
{
    /// <summary>
    /// List of API for Battleship game.
    /// API pertaining to single player.
    /// API Assumption : 
    /// 1. board for the user is always ready for game
    /// 2. user just need to start the game by placing the ship using - actions/place-ship
    /// 3. once required ships are placed on the board, user can start lodging attacks using - actions/attack
    /// 4. At point of time user can reset the game using - actions/reset-game
    /// APIs hosted at - https://ofxbattleshipapis.azurewebsites.net/api2/battleship/
    /// </summary>
    [RoutePrefix(GameConstant.ApiRoutePrefix + "battleship")]
    public class StateTrackerController : ApiController
    {
        private readonly IStateTrackingManager _stateTrackingManager;

        public StateTrackerController(IStateTrackingManager stateTrackingManager)
        {
            _stateTrackingManager = stateTrackingManager;
        }

        /// <summary>
        /// API to reset the board and start a new game
        /// </summary>
        /// <returns>status message</returns>
        [HttpPost, Route("actions/reset-game")]
        public IHttpActionResult ResetBoard()
        {
            _stateTrackingManager.ResetBoard();
            return Ok("Board is reset. Enjoy the new game!");
        }

        /// <summary>
        /// API to place a ship on the board 
        /// </summary>
        /// <param name="shipModel">Detail about the ship like orientation, type, start point location etc
        /// Sample Json for input - 
        /// {
        ///     ship:'Submarine',
        ///     startPoint:{xCoordinate:3,
        ///                 yCoordinate:3},
        ///     orientation:'north'
        /// }
        /// </param>
        /// <returns>Result with detail message of success/failure. In case of failure, shows the reason for failure</returns>
        [HttpPost, Route("actions/place-ship")]
        public IHttpActionResult PlaceShip(ShipModel shipModel)
        {
            if (ModelState.IsValid)
            {
                bool isSuccessfullyPlaced = false;
                var result = _stateTrackingManager.AddShipToBoard(shipModel, out isSuccessfullyPlaced);
                if (isSuccessfullyPlaced)
                    return Ok(result);
                else
                    return BadRequest(result);
            }
            return BadRequest(ModelState);
        }

        /// <summary>
        /// API to lodge an attack on board at a give location
        /// </summary>
        /// <param name="pointUnderAttack">Point of attack (X and Y details of the point)
        /// Sample Point Json for input     
        /// {
        ///         xCoordinate:3,
        ///         yCoordinate:4
        ///     }
        /// </param>
        /// <returns> Sample result - 
        /// {
        ///     "isGameOver": false,
        ///      "attackStatus": "Invalid",
        ///      "message": "Alas, No ships on the board. Please start placing ships on board and try again."
        /// }
        /// </returns>
        [HttpPost, Route("actions/attack")]
        public IHttpActionResult Attack(ShipPointLocationModel pointUnderAttack)
        {
            if (ModelState.IsValid)
            {
                var isGameOver = false;
                var attackResult = _stateTrackingManager.AttackOnBoard(pointUnderAttack, out isGameOver);
                return Ok(new
                {
                    IsGameOver = isGameOver,
                    AttackStatus = attackResult.ToString(),
                    Message = isGameOver ? "Board is reset for a new game as this game is over!" :
                                        attackResult.GetDescription()
                });
            }
            return BadRequest(ModelState);
        }
    }
}
