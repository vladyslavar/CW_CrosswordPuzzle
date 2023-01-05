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
    [TestClass]
    public class TestMainFormService
    {
        private static DBContext dbContext = new DBContext();
        private static DBActions dbActions = new DBActions(dbContext);
        MainFormService mainFormService = new MainFormService(dbActions);

        [TestMethod]
        public void TestGetCategories()
        {
            var categories = mainFormService.GetCategories();
            Assert.IsNotNull(categories);
        }
        [TestMethod]
        public void TestChangingSizeByCategory()
        {
            var theme = dbActions.GetAllCategories().FirstOrDefault();
            if(theme == null)
            {
                string categoryName = "testCat";
                Category category = new Category()
                {
                    Name = categoryName,
                };
                dbActions.AddCategory(category);

                theme = dbContext.Categories.Where(n => n.Name == categoryName).FirstOrDefault();
                Word addword = new Word()
                {
                    Name = "additionaltest",
                    Meaning = "addmeaning",
                    CategoryId = theme.Id
                };
                dbActions.AddWord(addword);
            }

            int firstSize = mainFormService.GetSizeByTheme(theme.Name).Size;
            Word word = new Word()
            {
                Name = "test",
                Meaning = "meaning",
                CategoryId = theme.Id
            };
            dbActions.AddWord(word);
            int secondsize = mainFormService.GetSizeByTheme(theme.Name).Size;
            var info = mainFormService.GetSizeByTheme(theme.Name);
            Assert.AreEqual(firstSize + 1, secondsize);
            Assert.AreEqual(theme.Name, info.Theme);

            var themeIdByName = dbActions.GetCategoryByName(theme.Name).FirstOrDefault().Id;
            var wordsByTheme = dbActions.GetWordByCategory(themeIdByName);
            foreach(var w in wordsByTheme)
            {
                if (w.Name == "test" || w.Name == "additionaltest") dbActions.DeleteWord(w.Id);
            }
            if (theme.Name == "testCat") dbActions.DeleteCategory(theme.Id);
        }
        [TestMethod]
        public void TestGettingSizeWithNullCategory()
        {
            var ret = mainFormService.GetSizeByTheme(null);
            Assert.IsNotNull(ret);
        }
    }
}
