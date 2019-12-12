using AutoMapper;
using Battleship.BL.Common;
using Battleship.BL.Entities;
using Battleship.BL.Entities.Interface;
using Battleship.BL.Logic.Interface;
using Battleship.BL.Models;
using System.Collections.Generic;
using System.Linq;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Logic
{
    /// <summary>
    /// BL layer manager for ship state tracking 
    /// </summary>
    public class StateTrackingManager : IStateTrackingManager
    {
        //TODO: Need to be moved to an appropriate persistence layer
        //STORAGE
        public static IList<IBattleship> ShipsOnBoard = new List<IBattleship>();

        //Dependancy
        private readonly IBattleShipFactory _battleShipFactory;

        //Constructor
        public StateTrackingManager(IBattleShipFactory battleShipFactory)
        {
            _battleShipFactory = battleShipFactory;
        }


        ///// ------------ Public methods ---------- START -------------///
        public void ResetBoard()
        {
            ShipsOnBoard.Clear();
        }

        public string AddShipToBoard(ShipModel shipToBePlaced, out bool isSuccessfullyPlaced)
        {
            var battleShip = _battleShipFactory.GetBattleship(shipToBePlaced.Ship);
            var errorMessage = IsValidPosition(battleShip, shipToBePlaced);
            if (string.IsNullOrEmpty(errorMessage) && battleShip.PointsOccupied != null && battleShip.PointsOccupied.Count > 0)
            {
                ShipsOnBoard.Add(battleShip);
                isSuccessfullyPlaced = true;
                return "Ship successfully placed at requested position.";
            }
            isSuccessfullyPlaced = false;
            return errorMessage;
        }

        public AttackResult AttackOnBoard(ShipPointLocationModel attackingPosition, out bool isGameOver)
        {
            isGameOver = false;

            //Check if there is any ship on the board 
            if (!ShipsOnBoard.Any())
                return AttackResult.Invalid;

            var attackStatus = AttackResult.Miss;
            var shipUnderAttack = ShipsOnBoard.FirstOrDefault(ship => ship.PointsOccupied.Any(p => p.XCoordinate == attackingPosition.XCoordinate && p.YCoordinate == attackingPosition.YCoordinate));
            if (shipUnderAttack != null)
            {
                shipUnderAttack.PointsAttacked = shipUnderAttack.PointsAttacked ?? new List<ShipPoint>();

                //Check to see if the same point is already attacked previously
                if (shipUnderAttack.PointsAttacked.Any(p => p.XCoordinate == attackingPosition.XCoordinate && p.YCoordinate == attackingPosition.YCoordinate))
                {
                    attackStatus = AttackResult.RepeatAttack;
                }
                else
                {

                    var pointUnderAttack = Mapper.Map<ShipPoint>(attackingPosition);
                    shipUnderAttack.PointsAttacked.Add(pointUnderAttack);
                    if (shipUnderAttack.Size == shipUnderAttack.PointsAttacked.Count)
                    {
                        shipUnderAttack.Health = ShipHealth.Sunk;
                        attackStatus = AttackResult.Sunk;
                    }
                    else
                    {
                        shipUnderAttack.Health = ShipHealth.Damaged;
                        attackStatus = AttackResult.Hit;
                    }
                }
            }

            //Game will not be over untill all the ship on board are are SUNK status
            isGameOver = !ShipsOnBoard.Any(ship => ship.Health == ShipHealth.Undamaged || ship.Health == ShipHealth.Damaged);

            //Reset the board if all the placed ships are sunk
            if (isGameOver)
                ResetBoard();

            return attackStatus;
        }
        ///// ------------ Public methods ---------- END -------------///


        ///// ------------ Private supporting methods ---------- START -------------///
        private string IsValidPosition(IBattleship battleship, ShipModel shipRequested)
        {
            var pointRequested = Mapper.Map<ShipPoint>(shipRequested.StartPoint);
            var newShipPoints = battleship.GetPointsOccupiedOnBoard(pointRequested, shipRequested.Orientation);

            //None of the Coordinate to fall outside of the board
            var invalidPosition = newShipPoints.Any(point => point.XCoordinate < 1 || point.XCoordinate > GameConstant.MaxXOnBoard ||
                                                point.YCoordinate < 1 || point.YCoordinate > GameConstant.MaxYOnBoard);

            if (invalidPosition)
            {
                return "Selected ship position and orientation collectively makes ship fall out of board.";
            }

            //New ship should not overlap with any of the existing ship
            invalidPosition = ShipsOnBoard.Any() && ShipsOnBoard.Any(placedShip => placedShip.PointsOccupied.Any(p => newShipPoints.Any(np => np.XCoordinate == p.XCoordinate && np.YCoordinate == p.YCoordinate)));

            if (invalidPosition)
            {
                return "Selected ship position and orientation collectively makes ship collide with another ship on board.";
            }

            battleship.PointsOccupied = newShipPoints;

            return string.Empty;
        }
        ///// ------------ Private supporting methods ---------- END -------------///
    }
}
