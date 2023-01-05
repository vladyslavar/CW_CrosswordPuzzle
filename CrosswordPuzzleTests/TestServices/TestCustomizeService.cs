using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.DataBase.DBEntities;
using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzleTests.TestServices
{
    [TestClass]
    public class TestCustomizeService
    {
        private static DBContext dbContext = new DBContext();
        private static DBActions dbActions = new DBActions(dbContext);
        CustomizeService customizeService = new CustomizeService(dbActions);

        [TestMethod]
        public void TestGetCategories()
        {
            var categories = customizeService.GetCategories();
            Assert.IsNotNull(categories);
        }
        [TestMethod]
        public void TestGetQuestionsByNotExistingTheme()
        {
            var theme = "xcfhgvg4111tgv15sf15fd";
            var questions = customizeService.GetQuestionsByTheme(theme);
            Assert.IsNotNull(questions);
        }
        [TestMethod]
        public void TestGetQuestionsByNyllTheme()
        {
            var questions = customizeService.GetQuestionsByTheme(null);
            Assert.IsNotNull(questions);
        }
        [TestMethod]
        public void TestDeleteThemeByNotValidName()
        {
            var name = "xcfhgvg4111tgv15sf15fd";
            try
            {
                var questions = customizeService.GetQuestionsByTheme(name);
            }
            catch (Exception e) { Assert.Fail(e.Message); }

            try
            {
                var questions = customizeService.GetQuestionsByTheme(null);
            }
            catch (Exception e) { Assert.Fail(e.Message); }
        }
        [TestMethod]
        public void TestAddAlreadyExistingTheme()
        {
            string name = "testCategory";
            Category category = new Category()
            {
                Name = name,
            };
            dbActions.AddCategory(category);
            var categByName = dbContext.Categories.Where(n => n.Name == name);

            customizeService.AddNewCategory(name);
            var secondCheck = dbContext.Categories.Where(n => n.Name == name);

            Assert.AreEqual(categByName.Count(), secondCheck.Count());

            int catId = dbContext.Categories.Where(n => n.Name == name).FirstOrDefault().Id;
            dbActions.DeleteCategory(catId);
        }
        [TestMethod]
        public void TestAddThemeByNotValidName()
        {
            var name = "";
            try
            {
                var questions = customizeService.GetQuestionsByTheme(name);
            }
            catch (Exception e) { Assert.Fail(e.Message); }

            try
            {
                var questions = customizeService.GetQuestionsByTheme(null);
            }
            catch (Exception e) { Assert.Fail(e.Message); }
        }
        [TestMethod]
        public void TestGettingSameWordsWhenNoSameWords()
        {
            string name = "cfvghjb84fg51bgf4bfg";
            string meaning = "jngfbv54vfdv1gf54v";

            var res = customizeService.GetSameWords(name, meaning);
            Assert.AreEqual(0, res.Count());
        }
        [TestMethod]
        public void TestGetSameWordByNotValidValues()
        {
            string name = "";
            string meaning = "";
            try
            {
                var words = customizeService.GetSameWords(name, meaning);
            }
            catch (Exception e) { Assert.Fail(e.Message); }

            try
            {
                var words = customizeService.GetSameWords(null, null);
            }
            catch (Exception e) { Assert.Fail(e.Message); }
        }
        [TestMethod]
        public void TestAddWordByNotValidName()
        {
            string name = "";
            string meaning = "";
            string theme = "";
            string notExistingTheme = "ynthbgrv4514nhGHvSCd";
            //empty values
            try
            {
                customizeService.AddNewWord(name, meaning, theme);
            }
            catch (Exception e) { Assert.Fail(e.Message); }
            //null values
            try
            {
                customizeService.AddNewWord(null, null, null);
            }
            catch (Exception e) { Assert.Fail(e.Message); }
            //not existing theme
            try
            {
                customizeService.AddNewWord("word", "meaning", notExistingTheme);
            }
            catch(Exception e) { Assert.Fail(e.Message); }
        }
        [TestMethod]
        public void TestDeleteWordWithNullValue()
        {
            try
            {
                customizeService.DeleteWords(null);
            }
            catch (Exception e) { Assert.Fail(e.Message); }
        }
    }
}

