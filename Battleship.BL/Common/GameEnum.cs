using System.ComponentModel;

namespace Battleship.BL.Common
{
    /// <summary>
    /// Battleship - Enums
    /// </summary>
    public static class GameEnum
    {
        public enum ShipType
        {
            AircraftCarrier,
            Battleship,
            Submarine,
            Cruiser,
            Destroyer
        }

        public enum AttackResult
        {
            [Description("It's a HIT!")]
            Hit,
            [Description("Opps!! it's a MISS this time :(. Better luck next time.")]
            Miss,
            [Description("Woohoo!! got one ship down :)")]
            Sunk,
            [Description("Alas, attacking the already damaged point. No benefits :| ")]
            RepeatAttack,
            [Description("Alas, No ships on the board. Please start placing ships on board and try again.")]
            Invalid
        }
        public enum ShipHealth
        {
            Damaged,
            Undamaged,
            Sunk
        }
        public enum ShipOrientation
        {
            North,
            East,
            South,
            West
        }
    }
}