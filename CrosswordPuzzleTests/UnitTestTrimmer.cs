using CrosswordPuzzle;
using CrosswordPuzzle.CrosswordLogic;
using CrosswordPuzzle.CrosswordLogic.Structs;

namespace CrosswordPuzzleTests
{
    [TestClass]
    public class UnitTestTrimmer
    {
        [TestMethod]
        public void TestArrayTrimming()
        {
            List<Word> startWords = new List<Word>()
            {
                new Word("word", new Possition(1, 2), false, 1, "word_meaning"),
                new Word("owl", new Possition(2, 2), true, 1, "owl_meaning"),
            };
            List<List<char>> startArr = new List<List<char>>()
            {
                new List<char>() { '\0', '\0', '\0', '\0', '\0'},
                new List<char>() { '\0', '\0', 'w', '\0', '\0'},
                new List<char>() { '\0', '\0', 'o', 'w', 'l'},
                new List<char>() { '\0', '\0', 'r', '\0', '\0'},
                new List<char>() { '\0', '\0', 'd', '\0', '\0'},
                new List<char>() { '\0', '\0', '\0', '\0', '\0'},
            };
            List<List<char>> targetArr = new List<List<char>>()
            {
                new List<char>() { 'w', '\0', '\0'},
                new List<char>() { 'o', 'w', 'l'},
                new List<char>() { 'r', '\0', '\0'},
                new List<char>() { 'd', '\0', '\0'},
            };
            ArrayTrimmer trimmer = new ArrayTrimmer();

            var result = trimmer.Trim(startArr, startWords);
            var arrayResults = result.array;

            Assert.AreEqual(targetArr.Count, arrayResults.Count);
            for (int i = 0; i < targetArr.Count; i++)
            {
                for (int j = 0; j < targetArr[0].Count; j++)
                {
                    Assert.AreEqual(targetArr[i][j], arrayResults[i][j]);
                }
            }
        }

        [TestMethod]
        public void TestWordsTrimming()
        {
            List<Word> startWords = new List<Word>()
            {
                new Word("word", new Possition(1, 2), false, 1, "word_meaning"),
                new Word("owl", new Possition(2, 2), true, 1, "owl_meaning"),
            };
            List<List<char>> startArr = new List<List<char>>()
            {
                new List<char>() { '\0', '\0', '\0', '\0', '\0'},
                new List<char>() { '\0', '\0', 'w', '\0', '\0'},
                new List<char>() { '\0', '\0', 'o', 'w', 'l'},
                new List<char>() { '\0', '\0', 'r', '\0', '\0'},
                new List<char>() { '\0', '\0', 'd', '\0', '\0'},
                new List<char>() { '\0', '\0', '\0', '\0', '\0'},
            };

            List<Word> targetWords = new List<Word>()
            {
                new Word("word", new Possition(0, 0), false, 1, "word_meaning"),
                new Word("owl", new Possition(1, 0), true, 1, "owl_meaning"),
            };

            ArrayTrimmer trimmer = new ArrayTrimmer();

            var result = trimmer.Trim(startArr, startWords);
            var wordsResults = result.words;

            for (int i = 0; i < wordsResults.Count; i++)
            {
                Assert.AreEqual(targetWords[i].word, wordsResults[i].word);
                Assert.AreEqual(targetWords[i].possition.X, wordsResults[i].possition.X);
                Assert.AreEqual(targetWords[i].possition.Y, wordsResults[i].possition.Y);
            }
        }
    }
}