using CrosswordPuzzle.CrosswordLogic.Structs;
using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.DataBase.DBEntities;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosswordPuzzle.Services
{
    public interface ICustomizeService
    {
        public void LoadDB();
        public void CloseDB();
        public List<string> GetCategories();
        public List<CrosswordPuzzle.DataBase.DBEntities.Word> GetQuestionsByTheme(string theme);
        public void DeleteCategory(string theme);
        public void AddNewCategory(string name);
        public List<string> GetSameWords(string name, string meaning);
        public void AddNewWord(string name, string meaning, string theme);
        public void DeleteWords(object word);
    }
    public class CustomizeService: ICustomizeService
    {
        private DBActions _dbActions;
        public CustomizeService(DBActions dbActions)
        {
            this._dbActions = dbActions;
        }
        public void LoadDB()
        {
            _dbActions.Load();
        }
        public void CloseDB()
        {
            _dbActions.Close();
        }
        public List<string> GetCategories()
        {
            var categories = _dbActions.GetAllCategories();
            List<string> strings = new List<string>();
            foreach (var category in categories) strings.Add(category.Name);
            return strings;
        }
        public List<CrosswordPuzzle.DataBase.DBEntities.Word> GetQuestionsByTheme(string theme)
        {
            List<CrosswordPuzzle.DataBase.DBEntities.Word> strings = new List<CrosswordPuzzle.DataBase.DBEntities.Word>();
            int catId;

            if (theme != null)
            {
                var cat = _dbActions.GetCategoryByName(theme).FirstOrDefault();
                if(cat != null)
                {
                    catId = cat.Id;
                    var questions = _dbActions.GetWordByCategory(catId);
                    foreach (var question in questions) strings.Add(question);
                }
            }
            return strings;
        }
        public void DeleteCategory(string theme)
        {
            int catId;
            if (theme != null && theme != "")
            {
                var cat = _dbActions.GetCategoryByName(theme).FirstOrDefault();
                if (cat != null)
                {
                    catId = cat.Id;
                    _dbActions.DeleteCategory(catId);
                }
            }
            //var catId = _dbActions.GetCategoryByName(theme).FirstOrDefault().Id;
        }
        public void AddNewCategory(string name)
        {
            if(name != "" && name != null)
            {
                var cat = _dbActions.GetCategoryByName(name).FirstOrDefault();
                if (cat == null)
                {
                    DataBase.DBEntities.Category category;
                    category = new DataBase.DBEntities.Category()
                    {
                        Name = name,
                    };
                    _dbActions.AddCategory(category);
                }
            }
        }
        public List<string> GetSameWords(string name, string meaning)
        {
            List<string> strings = new List<string>();
            if(name != "" && name != null && meaning != "" && meaning != null)
            {
                var sameWords = _dbActions.GetWordByNameAndMeaning(name, meaning);
                foreach (var word in sameWords) strings.Add(word.Name);
            }
            return strings;
        }
        public void AddNewWord(string name, string meaning, string theme)
        {
            DataBase.DBEntities.Word word;
            if(name != "" && name != null && meaning != "" && meaning != null && theme != "" && theme != null)
            {
                int catId;
                var cat = _dbActions.GetCategoryByName(theme).FirstOrDefault();
                if(cat != null)
                {
                    catId = cat.Id;
                    word = new DataBase.DBEntities.Word()
                    {
                        Name = name.ToLower(),
                        Meaning = meaning,
                        CategoryId = catId
                    };
                    _dbActions.AddWord(word);
                }
            }
        }
        public void DeleteWords(object word)
        {
            if(word != null)
            {
                Regex wordRegex = new Regex("^[a-z]+");
                Regex meaningRegex = new Regex("[a-z]+:\\s");
                var name = wordRegex.Match(word.ToString()).Value;
                var meaning = meaningRegex.Replace(word.ToString(), "");
                var words = _dbActions.GetWordByNameAndMeaning(name, meaning);

                foreach (var w in words)
                {
                    _dbActions.DeleteWord(w.Id);
                }
            }
        }
    }
}
