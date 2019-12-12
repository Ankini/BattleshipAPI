using Battleship.BL.Logic;
using Battleship.BL.Logic.Interface;
using NSubstitute;
using Xunit;
using AutoFixture;
using Battleship.BL.Entities.Interface;
using System.Collections.Generic;
using Battleship.BL.Entities.Ships;
using Battleship.BL.Entities;
using Battleship.BL.Models;
using static Battleship.BL.Common.GameEnum;
using Battleship.Tests.Util;
using AutoMapper;
using System.Linq;

namespace Battleship.Tests.BattleshipBLTests
{
    public class StateTrackingManagerTests
    {
        private readonly IBattleShipFactory _sutBattleShipFactory;
        private readonly IStateTrackingManager _sutStateTrackingManager;
        private readonly IShipOrientationFactory _sutShipOrientationFactory;

        private readonly IFixture _fixture;

        public StateTrackingManagerTests()
        {
            _fixture = new Fixture();
            _sutBattleShipFactory = Substitute.For<IBattleShipFactory>();
            _sutShipOrientationFactory = Substitute.For<IShipOrientationFactory>();
            _sutStateTrackingManager = new StateTrackingManager(_sutBattleShipFactory);

        }

        [Fact]
        public void ResetBoard_Should_Clear_All_The_Ships_Off_The_Board()
        {
            //arrange
            StateTrackingManager.ShipsOnBoard = SetupShipsOnBoard();


            //act
            _sutStateTrackingManager.ResetBoard();

            //assert
            Assert.Empty(StateTrackingManager.ShipsOnBoard);
        }

        [Fact]
        [ConfigureAutoMapper]
        public void AddShipToBoard_Should_Fail_And_Not_Add_Ship_To_Board_When_Collide_With_Another_Ship_On_Board()
        {
            //arrange
            bool isSuccess = false;
            StateTrackingManager.ShipsOnBoard = SetupShipsOnBoard();
            var shipsOnBoardCount = StateTrackingManager.ShipsOnBoard.Count;
            var shipModel = GetShipModel(ShipType.Battleship, ShipOrientation.South);
            var battleShip = Substitute.For<IBattleship>();
            _sutBattleShipFactory.GetBattleship(ShipType.Battleship).Returns(battleShip);
            var pointRequested = Mapper.Map<ShipPoint>(shipModel.StartPoint);
            var pointOccupied = StateTrackingManager.ShipsOnBoard?.FirstOrDefault().PointsOccupied;
            battleShip.GetPointsOccupiedOnBoard(Arg.Any<ShipPoint>(), Arg.Any<ShipOrientation>()).Returns(pointOccupied);

            //act
            var message = _sutStateTrackingManager.AddShipToBoard(shipModel, out isSuccess);
            var shipsOnBoardCountAfterPlacingNew = StateTrackingManager.ShipsOnBoard.Count;

            //assert
            Assert.False(isSuccess);
            Assert.Equal("Selected ship position and orientation collectively makes ship collide with another ship on board.", message);
            Assert.Equal(shipsOnBoardCountAfterPlacingNew, shipsOnBoardCount);
        }

        [Fact]
        [ConfigureAutoMapper]
        public void AddShipToBoard_Should_Fail_And_Not_Add_Ship_To_Board_When_Specification_Requested_Makes_It_Fall_Off_The_Board()
        {
            //arrange
            bool isSuccess = false;
            StateTrackingManager.ShipsOnBoard = SetupShipsOnBoard();
            var shipsOnBoardCount = StateTrackingManager.ShipsOnBoard.Count;
            var shipModel = GetShipModel(ShipType.Battleship, ShipOrientation.South);
            var battleShip = Substitute.For<IBattleship>();
            _sutBattleShipFactory.GetBattleship(ShipType.Battleship).Returns(battleShip);
            var pointRequested = Mapper.Map<ShipPoint>(shipModel.StartPoint);
            var pointOccupied = _fixture.CreateMany<ShipPoint>().ToList();
            pointOccupied.ForEach(p => p.XCoordinate = p.XCoordinate - 10);
            battleShip.GetPointsOccupiedOnBoard(Arg.Any<ShipPoint>(), Arg.Any<ShipOrientation>()).Returns(pointOccupied);

            //act
            var message = _sutStateTrackingManager.AddShipToBoard(shipModel, out isSuccess);
            var shipsOnBoardCountAfterPlacingNew = StateTrackingManager.ShipsOnBoard.Count;

            //assert
            Assert.False(isSuccess);
            Assert.Equal("Selected ship position and orientation collectively makes ship fall out of board.", message);
            Assert.Equal(shipsOnBoardCountAfterPlacingNew, shipsOnBoardCount);
        }

        [Fact]
        [ConfigureAutoMapper]
        public void AddShipToBoard_Should_Pass_And_Add_Ship_To_Board_When_Specification_Requested_Is_Valid()
        {
            //arrange
            StateTrackingManager.ShipsOnBoard = new List<IBattleship>();
            bool isSuccess = false;

            var shipsOnBoardCount = StateTrackingManager.ShipsOnBoard.Count;
            var shipModel = GetShipModel(ShipType.Battleship, ShipOrientation.South);
            var battleShip = Substitute.For<IBattleship>();
            _sutBattleShipFactory.GetBattleship(ShipType.Battleship).Returns(battleShip);
            var pointRequested = Mapper.Map<ShipPoint>(shipModel.StartPoint);
            var pointsOccupiedByDestroyer = new List<ShipPoint>();
            pointsOccupiedByDestroyer.Add(new ShipPoint() { XCoordinate = 5, YCoordinate = 1 });
            pointsOccupiedByDestroyer.Add(new ShipPoint() { XCoordinate = 5, YCoordinate = 2 });
            battleShip.GetPointsOccupiedOnBoard(Arg.Any<ShipPoint>(), Arg.Any<ShipOrientation>()).Returns(pointsOccupiedByDestroyer);

            //act
            var message = _sutStateTrackingManager.AddShipToBoard(shipModel, out isSuccess);
            var shipsOnBoardCountAfterPlacingNew = StateTrackingManager.ShipsOnBoard.Count;

            //assert
            Assert.True(isSuccess);
            Assert.Equal("Ship successfully placed at requested position.", message);
            Assert.Equal(shipsOnBoardCountAfterPlacingNew, shipsOnBoardCount + 1);
        }

        [Theory]
        [ConfigureAutoMapper]
        [MemberData(nameof(ShipPointLocationModelList))]
        public void AttackOnBoard_Should_Result_With_Correct_AttackResultStatus(ShipPointLocationModel attackingPosition, AttackResult attackResult
             ,bool needemptyBoard, bool configureBoard, bool isSunk, bool shouldGameOver)
        {
            //arrange
            if (configureBoard)
                StateTrackingManager.ShipsOnBoard = SetupShipsOnBoard();
            if(needemptyBoard)
                StateTrackingManager.ShipsOnBoard= new List<IBattleship>();

            var isGameOver = false;

            //act
            var result = _sutStateTrackingManager.AttackOnBoard(attackingPosition, out isGameOver);
            var shipUnderAttack = StateTrackingManager.ShipsOnBoard.FirstOrDefault(ship => ship.PointsOccupied.Any(p => p.XCoordinate == attackingPosition.XCoordinate && p.YCoordinate == attackingPosition.YCoordinate));

            //assert
            Assert.Equal(attackResult, result);
            if (isSunk && !shouldGameOver) //After the last ship sunk, game will be reset with no ships on board. so shipUnderAttack would not be available
            {
                Assert.Equal(ShipHealth.Sunk, shipUnderAttack.Health);
            }
            else if (attackResult == AttackResult.Hit)
            {
                Assert.Equal(ShipHealth.Damaged, shipUnderAttack.Health);
            }
            Assert.Equal(shouldGameOver, isGameOver);
            //if(shouldGameOver)
            //Assert.Equal(0, StateTrackingManager.ShipsOnBoard.Count);
        }


        // Member Data ----- START ---------------// 
        public static IEnumerable<object[]> ShipPointLocationModelList()
        {
            yield return new object[]
            {
                new ShipPointLocationModel(){ XCoordinate=6,YCoordinate=6},
                AttackResult.Invalid,
                true,  //Test on Empty board
                false, //Should Reset Board
                false, //Should the ship sunk after this attack
                false  //Should the game be over after this attack
            };
            yield return new object[]
            {
                new ShipPointLocationModel(){ XCoordinate=1,YCoordinate=1},
                AttackResult.Hit,
                false,
                true,
                false,
                false
            };
            yield return new object[]
            {
                new ShipPointLocationModel(){ XCoordinate=5,YCoordinate=1},
                AttackResult.Hit,
                false,
                false,
                false,
                false
            };
            yield return new object[]
            {
                new ShipPointLocationModel(){ XCoordinate=5,YCoordinate=2},
                AttackResult.Sunk,
                false,
                false,
                true,
                false
            };
            yield return new object[]
            {
                new ShipPointLocationModel(){ XCoordinate=6,YCoordinate=6},
                AttackResult.Miss,
                false,
                false,
                false,
                false
            };
            yield return new object[]
           {
                new ShipPointLocationModel(){ XCoordinate=1,YCoordinate=2},
                AttackResult.Hit,
                false,
                false,
                false,
                false
           };

            yield return new object[]
           {
                new ShipPointLocationModel(){ XCoordinate=1,YCoordinate=3},
                AttackResult.Sunk,
                false,
                false,
                true,
                true
           };

        }
        // Member Data ----- END ---------------//


        /// --- Supporting Methods -- Private functions -------------------- START -------------- //
        private ShipModel GetShipModel(ShipType shipType, ShipOrientation orientation, int x = 1, int y = 1)
        {
            var shipModel = new ShipModel();
            shipModel.Ship = shipType;
            shipModel.Orientation = orientation;
            shipModel.StartPoint = new ShipPointLocationModel() { XCoordinate = x, YCoordinate = y };
            return shipModel;
        }
        private IList<IBattleship> SetupShipsOnBoard()
        {
            var pointsOccupiedBySubmarine = new List<ShipPoint>();
            pointsOccupiedBySubmarine.Add(new ShipPoint() { XCoordinate = 1, YCoordinate = 1 });
            pointsOccupiedBySubmarine.Add(new ShipPoint() { XCoordinate = 1, YCoordinate = 2 });
            pointsOccupiedBySubmarine.Add(new ShipPoint() { XCoordinate = 1, YCoordinate = 3 });

            var pointsOccupiedByDestroyer = new List<ShipPoint>();
            pointsOccupiedByDestroyer.Add(new ShipPoint() { XCoordinate = 5, YCoordinate = 1 });
            pointsOccupiedByDestroyer.Add(new ShipPoint() { XCoordinate = 5, YCoordinate = 2 });

            return new List<IBattleship>()
                 {
                     new DestroyerShip(_sutShipOrientationFactory){PointsOccupied=pointsOccupiedByDestroyer},
                     new SubmarineShip(_sutShipOrientationFactory){PointsOccupied=pointsOccupiedBySubmarine}
                 };
        }
        /// --- Supporting Methods -- Private functions -------------------- End -------------- //
    }
}
