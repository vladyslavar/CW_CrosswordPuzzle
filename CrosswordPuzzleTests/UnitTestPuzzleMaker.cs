using CrosswordPuzzle.CrosswordLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ICSharpCode.SharpZipLib.Zip.ExtendedUnixData;

namespace CrosswordPuzzleTests
{
    [TestClass]
    public class UnitTestPuzzleMaker
    {

        Dictionary<string, string> mainDictionary = new  Dictionary<string, string>()
        {
            ["firstword"] = "firstMeaning",
            ["secondword"] = "secondMeaning",
            ["thirdword"] = "thirdMeaning",
            ["virologist"] = "",
            ["variable"] = "",
            ["tissue"] = "",
            ["theory"] = "",
            ["telescope"] = "",
            ["seismology"] = "",
            ["scientist"] = "",
            ["retort"] = "",
            ["observatory"] = "",
            ["neiron"] = "",
            ["molecule"] = "",
            ["mineral"] = "",
            ["magnetism"] = "",
            ["ichthyology"] = "",
            ["hypothesis"] = "",
            ["herpetology"] = "",
            ["cheetah"] = "",

        };
        Dictionary<string, string> additionalDictionary = new Dictionary<string, string>()
        {
            ["addfirstword"] = "addfirstMeaning",
            ["addsecondword"] = "addsecondMeaning",
            ["addthirdword"] = "addthirdMeaning",
            ["funnel"] = "",
            ["fossil"] = "",
            ["flask"] = "",
            ["experiment"] = "",
            ["evolution"] = "",
            ["cuvette"] = "",
        };
        [TestMethod]
        public void TestEmptyMainDictionary()
        {
            Dictionary<string, string> mDictionary = new Dictionary<string, string>();
            PuzzleMaker puzzleMaker = new PuzzleMaker();
            var output = puzzleMaker.Puzzle(mDictionary, additionalDictionary);

            Assert.IsNotNull(output);
            Assert.AreEqual(mDictionary.Count, output.Count);
        }
        [TestMethod]
        public void TestNullMainDictionary()
        {
            PuzzleMaker puzzleMaker = new PuzzleMaker();
            var output = puzzleMaker.Puzzle(null, additionalDictionary);

            Assert.IsNotNull(output);
            Assert.AreEqual(0, output.Count);
        }
        [TestMethod]
        public void TestEmptyAdditionalDictionary()
        {
            Dictionary<string, string> addDictionary = new Dictionary<string, string>();
            
            PuzzleMaker puzzleMaker = new PuzzleMaker();
            var output = puzzleMaker.Puzzle(mainDictionary, addDictionary);

            Assert.IsNotNull(output);
            Assert.AreNotEqual(0, output.Count);
        }
        [TestMethod]
        public void TestNullAdditionalDictionary()
        {
            PuzzleMaker puzzleMaker = new PuzzleMaker();

            var output = puzzleMaker.Puzzle(mainDictionary, null);
            Assert.IsNotNull(output);
            Assert.AreNotEqual(0, output.Count);
        }
        [TestMethod]
        public void TestReturnArraySize()
        {
            var expectedSize = mainDictionary.Count * 10;
            PuzzleMaker puzzleMaker = new PuzzleMaker();

            var output = puzzleMaker.Puzzle(mainDictionary, additionalDictionary);
            
            Assert.AreEqual(expectedSize, output.Count);
        }
        [TestMethod]
        public void TestAtLeastOneWordIsPlaced()
        {
            PuzzleMaker puzzleMaker = new PuzzleMaker();

            var output = puzzleMaker.Puzzle(mainDictionary, additionalDictionary);
            var wordsOut = puzzleMaker.words;
            bool isAnyWord = false;
            foreach(var col in output)
            {
                foreach(var el in col)
                {
                    if (el != '\0')
                    {
                        isAnyWord = true;
                        break;
                    }
                    if (isAnyWord) break;
                }
            }

            Assert.AreNotEqual(0, wordsOut.Count);
            Assert.IsTrue(isAnyWord);
        }
        [TestMethod]
        public void TestSpeedOfPuzzleMaker()
        {
            // 20 words
            PuzzleMaker puzzleMaker = new PuzzleMaker();
            var startTime = DateTime.Now;
            var output = puzzleMaker.Puzzle(mainDictionary, additionalDictionary);
            var endTime = DateTime.Now;
            bool isFullymadePuzzle = puzzleMaker.words.Count() > 18 ? true : false;
            Assert.IsTrue(isFullymadePuzzle);
            if (endTime.Minute == startTime.Minute && endTime.Second == startTime.Second)
            {
                if (endTime.Millisecond - startTime.Millisecond < 10)
                {
                    Assert.AreEqual(0, 0);
                }
                else Assert.Fail();
            }
            else Assert.Fail();
        }
    }
}
