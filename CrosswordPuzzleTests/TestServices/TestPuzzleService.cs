using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.DataBase.DBEntities;
using CrosswordPuzzle.Presentors;
using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzleTests.TestServices
{
    /// SetPuzzle not required to test
    
    [TestClass]
    public class TestPuzzleService
    {
        private DBContext dbContext;
        private DBActions dbActions;
        PuzzleService puzzleService;

        public TestPuzzleService()
        {
            dbContext = new DBContext();
            dbActions = new DBActions(dbContext);
            dbContext.Database.EnsureCreated();

            puzzleService = new PuzzleService(dbActions);
        }

        [TestMethod]
        public void TestInitializedDictionariesReturnedSize()
        {
            int size = 5;
            var useCategory = InitializingEnvironment(size);

            var dicts = puzzleService.InitializeDictionaries(size, useCategory.Name);
            Assert.AreEqual(size / 3, dicts.additionalDictionary.Count());
            Assert.AreEqual(size, dicts.obligatoryDictionary.Count());

            var wordsToDelete = dbActions.GetWordByCategory(useCategory.Id);
            foreach (var word in wordsToDelete) dbActions.DeleteWord(word.Id);
            foreach (var c in dbActions.GetCategoryByName(useCategory.Name)) dbActions.DeleteCategory(c.Id);
        }
        [TestMethod]
        public void TestWordsInObligatoryAndAdditionalDictionariesNotRepead()
        {
            int size = 5;
            var useCategory = InitializingEnvironment(size);
            bool isRepeated = false;

            var dicts = puzzleService.InitializeDictionaries(size, useCategory.Name);
            foreach(var oblDictEl in dicts.obligatoryDictionary)
            {
                foreach(var addDictEl in dicts.additionalDictionary)
                {
                    if(oblDictEl.Key == addDictEl.Key && oblDictEl.Value == addDictEl.Value)
                    {
                        isRepeated = true;
                        break;
                    }
                }
                if (isRepeated) break;
            }
            Assert.IsFalse(isRepeated);
            var wordsToDelete = dbActions.GetWordByCategory(useCategory.Id);
            foreach (var word in wordsToDelete) dbActions.DeleteWord(word.Id);
            foreach(var c in dbActions.GetCategoryByName(useCategory.Name)) dbActions.DeleteCategory(c.Id);
        }
        public Category InitializingEnvironment(int size)
        {
            Category useCategory = null;
            string catName = "newTestC";

            var categories = dbActions.GetAllCategories();
            if (categories.Count() > 0)
            {
                foreach (var category in categories)
                {
                    var words = dbActions.GetWordByCategory(category.Id);
                    if (words.Count() >= 3 * (size + (size / 3)))
                    {
                        useCategory = category;
                        break;
                    }
                }
            }
            if (useCategory == null)
            {
                Category newC = new Category()
                {
                    Name = catName
                };
                dbActions.AddCategory(newC);
                for (int i = 0; i < 3 * (size + (size / 3)); i++)
                {
                    string name = "testName" + i.ToString();
                    string meaning = "testMeaning" + i.ToString();
                    int cId = dbActions.GetCategoryByName(catName).First().Id;
                    Word newW = new Word()
                    {
                        Name = name,
                        Meaning = meaning,
                        CategoryId = cId
                    };
                    dbActions.AddWord(newW);
                }
                useCategory = dbActions.GetCategoryByName(catName).First();
            }
            return useCategory;
        }
    }
}
