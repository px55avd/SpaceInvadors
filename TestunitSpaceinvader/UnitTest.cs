using Microsoft.VisualStudio.TestTools.UnitTesting;
using SpaceInvaders;
using System;

namespace TestunitSpaceinvader
{
    [TestClass]
    public class UnitTest
    {

        [TestMethod]
        public void CreateGameInstance()
        {
            // Arrange
            Game game;

            // Act
            game = new Game();

            // Assert
            Assert.IsNotNull(game);
        }

        [TestMethod]
        public void CreateMenuInstance()
        {
            // Arrange
            Menu menu;

            // Act
            menu = new Menu();


            // Assert
            Assert.IsNotNull(menu);
        }
    }
}

