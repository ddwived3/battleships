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
            var ships = new[] { "3:1,6:1" };
            var guesses = new[] { "3:1", "4:1", "5:1", "6:1" };
            Game.Play(ships, guesses).Should().Be(1);
        }

        [Fact]
        public void TestPlay_HitAll_Sunk2CellShips()
        {
            var ships = new[] { "1:1,2:1", "3:2,3:3" };
            var guesses = new[] { "1:1", "2:1", "3:2", "3:3" };
            Game.Play(ships, guesses).Should().Be(2);
        }

        [Fact]
        public void TestPlay_HitAll_SinkShipCount()
        {
            var ships = new[] { "3:2,3:5" };
            var guesses = new[] { "3:2", "3:3", "3:4" };
            Game.Play(ships, guesses).Should().Be(0);
        }
    }
}
