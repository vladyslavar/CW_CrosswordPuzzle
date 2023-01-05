using CrosswordPuzzle.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrosswordPuzzle.DataBase.DBEntities;

namespace CrosswordPuzzleTests
{
    [TestClass]
    public class UnitTestDataBase
    {
        // only possitive testing. Checking for exeptions in services and views
        private static DBContext dbContext = new DBContext();
        private DBActions dbActions = new DBActions(dbContext);
        private string categoryName = "TestCateg";
        private string wordName = "TestWord";
        private string wordMeaning = "meaning";

        [TestMethod]
        public void TestDBloading()
        {
            dbActions.Load();
            // if db already exists isJustCreated = false
            bool isJustCreated = dbContext.Database.EnsureCreated();
            Assert.IsFalse(isJustCreated);
        }
        [TestMethod]
        public void TestDBCategory()
        {
            // ADD
            Category category = new Category()
            {
                Name = categoryName,
            };
            dbActions.AddCategory(category);

            var categByName = dbContext.Categories.Where(n => n.Name == categoryName).FirstOrDefault();
            Assert.AreEqual(categoryName, categByName.Name);

            // GET

            // BY NAME
            var categsFromDB = dbActions.GetCategoryByName(categoryName);

            Assert.AreEqual(1, categsFromDB.Count());
            Assert.AreEqual(categoryName, categsFromDB.FirstOrDefault().Name);

            //BY ID
            categByName = dbContext.Categories.Where(n => n.Name == categoryName).FirstOrDefault();
            categsFromDB = dbActions.GetCategoryById(categByName.Id);

            Assert.AreEqual(1, categsFromDB.Count());
            Assert.AreEqual(categoryName, categsFromDB.FirstOrDefault().Name);

            // ALL CATEGORIES
            categsFromDB = dbActions.GetAllCategories();

            bool containsTestCateg = false;
            foreach (var categ in categsFromDB)
            {
                if (categ.Name == categoryName) containsTestCateg = true;
                if (containsTestCateg) break;
            }
            bool containsOneOrMore = categsFromDB.Count() >= 1 ? true : false;
            Assert.IsTrue(containsOneOrMore);
            Assert.IsTrue(containsTestCateg);

            // DELETE
            var categsByName = dbContext.Categories.Where(n => n.Name == categoryName);

            foreach(var categ in categsByName)
            {
                dbActions.DeleteCategory(categ.Id);
            }
            categsByName = dbContext.Categories.Where(n => n.Name == categoryName);
            Assert.AreEqual(0, categsByName.Count());
        }

        [TestMethod]
        public void TestDBWord()
        {
            // ADD CATEGORY
            Category category = new Category()
            {
                Name = categoryName,
            };
            dbActions.AddCategory(category);

            var categsByName = dbContext.Categories.Where(n => n.Name == categoryName);

            Assert.AreEqual(1, categsByName.Count());
            Assert.AreEqual(categoryName, categsByName.FirstOrDefault().Name);

            // ADD WORD
            Word word = new Word()
            {
                Name = wordName,
                Meaning = "meaning",
                CategoryId = categsByName.FirstOrDefault().Id
            };
            dbActions.AddWord(word);

            var wordsFromDB = dbContext.Words.Where(n => n.Name == wordName);

            Assert.AreEqual(1, wordsFromDB.Count());
            Assert.AreEqual(wordName, wordsFromDB.FirstOrDefault().Name);

            // GET WORDS

            // BY NAME
            wordsFromDB = dbActions.GetWordByName(wordName);

            Assert.AreEqual(1, wordsFromDB.Count());
            Assert.AreEqual(wordName, wordsFromDB.FirstOrDefault().Name);
            Assert.AreEqual(wordMeaning, wordsFromDB.FirstOrDefault().Meaning);

            // BY ID
            var wordByName = dbContext.Words.Where(n => n.Name == wordName).FirstOrDefault();
            var wordFromDB = dbActions.GetWordById(wordByName.Id);

            Assert.AreEqual(wordName, wordFromDB.Name);
            Assert.AreEqual(wordMeaning, wordFromDB.Meaning);

            // BY NAME AND MEANING
            wordsFromDB = dbActions.GetWordByNameAndMeaning(wordName, wordMeaning);

            Assert.AreEqual(1, wordsFromDB.Count());
            Assert.AreEqual(wordName, wordsFromDB.FirstOrDefault().Name);
            Assert.AreEqual(wordMeaning, wordsFromDB.FirstOrDefault().Meaning);

            // CATEGORY
            var categByName = dbContext.Categories.Where(n => n.Name == categoryName).FirstOrDefault();

            wordsFromDB = dbActions.GetWordByCategory(categByName.Id);

            Assert.AreEqual(1, wordsFromDB.Count());
            Assert.AreEqual(wordName, wordsFromDB.FirstOrDefault().Name);
            Assert.AreEqual(wordMeaning, wordsFromDB.FirstOrDefault().Meaning);

            //DELETE WORDS
            var wordsByName = dbContext.Words.Where(n => n.Name == wordName && n.Meaning == wordMeaning);

            foreach (var w in wordsByName)
            {
                dbActions.DeleteWord(w.Id);
            }
            wordsByName = dbContext.Words.Where(n => n.Name == wordName);
            Assert.AreEqual(0, wordsByName.Count());

            // DELETE CATEGORY
            categsByName = dbContext.Categories.Where(n => n.Name == categoryName);

            foreach (var categ in categsByName)
            {
                dbActions.DeleteCategory(categ.Id);
            }
            categsByName = dbContext.Categories.Where(n => n.Name == categoryName);
            Assert.AreEqual(0, categsByName.Count());
        }
    }
}
