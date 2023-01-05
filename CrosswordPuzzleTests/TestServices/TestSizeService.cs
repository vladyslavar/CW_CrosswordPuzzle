using CrosswordPuzzle.CrosswordLogic.Structs;
using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzleTests.TestServices
{
    [TestClass]
    public class TestSizeService
    {
        private static DBContext dbContext = new DBContext();
        private static DBActions dbActions = new DBActions(dbContext);
        SizeService sizeService = new SizeService(dbActions);

        [TestMethod]
        public void TestInvalidValues()
        {
            int smallValue = 0;
            int bigValue = 100;

            int smallOut = sizeService.MaxAvailableForPuzzleWords(smallValue);
            int bigOut = sizeService.MaxAvailableForPuzzleWords(bigValue);

            Assert.AreEqual(-1, smallOut);
            Assert.AreEqual(50, bigOut);
        }
        [TestMethod]
        public void TestValidValues()
        {
            int value = 40;
            int expected = 40 - (40 % 5) - Convert.ToInt32(value / 3);

            int outValue = sizeService.MaxAvailableForPuzzleWords(value);

            Assert.AreEqual(expected, outValue);
        }
    }
}
