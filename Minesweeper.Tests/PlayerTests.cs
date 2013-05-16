using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Minesweeper.Tests
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Player's name must not be empty!")]
        public void TestPlayer_NullName()
        {
            Player player = new Player(null, 20);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Player's score must not be negative value!")]
        public void TestPlayer_EmptyName()
        {
            Player player = new Player("John Doe", -50);
        }

        [TestMethod]
        public void TestPlayer_Correct()
        {
            Player player = new Player("John Doe", 15);

            Assert.AreEqual("John Doe", player.PlayerName);
            Assert.AreEqual(15, player.PlayerPoints);
        }
    }
}
