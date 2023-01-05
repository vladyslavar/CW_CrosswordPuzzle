using CrosswordPuzzle.DataBase;
using CrosswordPuzzle.Presentors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrosswordPuzzle.Services
{
    public interface IMainFormService
    {
        public List<string> GetCategories();
        public ThemeSizeCount GetSizeByTheme(string theme);
        public void LoadDB();
        public void CloseDB();

    }
    public class MainFormService : IMainFormService
    {
        private DBActions _dbActions;
        public MainFormService(DBActions dbActions)
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

        public ThemeSizeCount GetSizeByTheme(string theme)
        {
            int words = 0;
            if (theme != null)
            {
                var catId = _dbActions.GetCategoryByName(theme).FirstOrDefault().Id;
                words = _dbActions.GetWordByCategory(catId).Count();
            }
            else
            {
                var availableCats = _dbActions.GetAllCategories();
                if(availableCats.Count() > 0)
                    theme = _dbActions.GetAllCategories().FirstOrDefault().Name;
                else
                    theme = "ERROR theme!";
            }
            return new ThemeSizeCount(theme, words);
        }
    }
}
