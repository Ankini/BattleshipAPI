using Battleship.BL.Entities.Interface;
using static Battleship.BL.Common.GameEnum;

namespace Battleship.BL.Logic.Interface
{
    public interface IBattleShipFactory
    {
        IBattleship GetBattleship(ShipType shipType);
    }
}
