using MinefieldKata.Models;
using MinefieldKata.Utilities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinefieldKata.Tests.Models
{
    public class MineLayerTests
    {
        private Mock<IRandomProvider> mockRandom;
        private Mock<IMap> mockMap;
        private MinePositionGenerator mineLayer;

        public MineLayerTests()
        {
            mockRandom = new Mock<IRandomProvider>();
            mockMap = new Mock<IMap>();

            mineLayer = new MinePositionGenerator(mockRandom.Object);
        }

        [Fact]
        public void WhenLayingZeroMines_AnEmptyListIsReturned()
        {
            var result = mineLayer.Generate(mockMap.Object, 0);
            Assert.Empty(result);
        }

        [Fact]
        public void WhenASquareIsAlreadyMined_SkipItAndRetryToPlaceTheSameMineAgain()
        {
            mockRandom.SetupSequence(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
              .Returns(3)
              .Returns(3)
              .Returns(4)
              .Returns(4);

            mockMap.SetupSequence(x => x.IsMine(It.IsAny<Position>()))
                .Returns(true)
                .Returns(false);

            var result = mineLayer.Generate(mockMap.Object, 1);

            Assert.Single(result);
            Assert.Equal(4, result.First().X);
            Assert.Equal(4, result.First().Y);
        }

        [Fact]
        public void WhenLayingAMine_TheProcessIsRepeatedUntilAllMinesArePlaced()
        {
            var mockMap = new Mock<IMap>();

            mockRandom.SetupSequence(x => x.Next(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(4)
                .Returns(4)
                .Returns(5)
                .Returns(5);

            mockMap.Setup(x => x.IsMine(It.IsAny<Position>()))
              .Returns(false);

            var result = mineLayer.Generate(mockMap.Object, 2);

            Assert.Equal(2, result.Count);
        }
    }
}
