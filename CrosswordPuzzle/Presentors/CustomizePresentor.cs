using CrosswordPuzzle.DataBase.DBEntities;
using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Presentors
{
    public class CustomizePresentor
    {
        public ICustomizeFormView _customizeFormView;
        public ICustomizeService _customizeService;

        public ICustomizeFormView CustomizeFormView { get { return _customizeFormView; } }
        public CustomizePresentor(ICustomizeFormView customizeFormView, ICustomizeService customizeService)
        {
            _customizeFormView = customizeFormView;
            _customizeService = customizeService;

            _customizeFormView.LoadDbHandler += LoadDB;
            _customizeFormView.CloseDbHandler += CloseDB;
            _customizeFormView.GetCategoriesHandler += GetCategories;
            _customizeFormView.GetQuestionsByThemeHandler += GetQuestionsByTheme;
            _customizeFormView.DeleteThemeByNameHandler += DeleteCategory;
            _customizeFormView.AddNewThemeHandler += AddNewCategory;
            _customizeFormView.GetSameWordsHandler += GetSameWords;
            _customizeFormView.AddNewWordHandler += AddNewWord;
            _customizeFormView.DeleteWordHandler += DeleteWords;

        }
        public void Run()
        {
            _customizeFormView.FormRun();
        }
        public void LoadDB()
        {
            _customizeService.LoadDB();
        }
        public void CloseDB()
        {
            _customizeService.CloseDB();
        }
        public void SwitchToCustomizeForm(object sender, EventArgs e)
        {
            _customizeFormView.FormShow();
        }

        public List<string> GetCategories()
        {
            return _customizeService.GetCategories();
        }
        public List<CrosswordPuzzle.DataBase.DBEntities.Word> GetQuestionsByTheme(string theme)
        {
            var res = _customizeService.GetQuestionsByTheme(theme);
            return res;
        }
        public void DeleteCategory(string theme)
        {
            _customizeService.DeleteCategory(theme);
        }
        public void AddNewCategory(string name)
        {
            _customizeService.AddNewCategory(name);
        }
        public List<string> GetSameWords(string name, string meaning)
        {
            return _customizeService.GetSameWords(name, meaning);
        }
        public void AddNewWord(string name, string meaning, string theme)
        {
            _customizeService.AddNewWord(name, meaning, theme);
        }
        public void DeleteWords(object word)
        {
            _customizeService.DeleteWords(word);
        }
    }
}
