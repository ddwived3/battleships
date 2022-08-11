using FluentAssertions;
using Xunit;

namespace Battleships.Test
{
    public class GameTest
    {
        [Fact]
        public void TestPlay()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "7:0", "3:3" };
            Game.Play(ships, guesses).Should().Be(0);
        }

        [Fact]
        public void TestPlay_HitAll_Sunk4CellShip()
        {
            var ships = new[] { "5:1,8:1" };
            var guesses = new[] { "5:1", "6:1", "7:1", "8:1" };
            Game.Play(ships, guesses).Should().Be(1);
        }

        [Fact]
        public void TestPlay_HitAll_Sunk2CellShips()
        {
            var ships = new[] { "0:1,1:1", "6:4,6:5" };
            var guesses = new[] { "0:1", "1:1", "6:4", "6:5" };
            Game.Play(ships, guesses).Should().Be(2);
        }

        [Fact]
        public void TestPlay_HitAll_SinkShipCount()
        {
            var ships = new[] { "9:3,9:6" };
            var guesses = new[] { "9:3", "9:4", "9:5" };
            Game.Play(ships, guesses).Should().Be(0);
        }
    }
}
