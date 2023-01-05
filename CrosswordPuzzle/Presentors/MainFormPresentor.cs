using CrosswordPuzzle.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrosswordPuzzle.Presentors
{
    public  class MainFormPresentor
    {
        IMainFormView _mainFormView;
        IMainFormService _mainFormService;

        public IMainFormView MainFormView { get { return _mainFormView; } }
        public MainFormPresentor(IMainFormView mainFormView, IMainFormService mainFormService)
        {
            _mainFormView = mainFormView;
            _mainFormService = mainFormService;

            _mainFormView.GetCategoriesHandler = GetCategories;
            _mainFormView.GetSizeHandler = GetSizeByTheme;
            _mainFormView.LoadDbHandler = LoadDB;
            _mainFormView.CloseDbHandler = CloseDB;
        }
        public void Run()
        {
            _mainFormView.FormRun();
        }
        public void LoadDB()
        {
            _mainFormService.LoadDB();
        }
        public void CloseDB()
        {
            _mainFormService.CloseDB();
        }
        public void SwitchToMainForm(object sender, EventArgs e)
        {
            _mainFormView.FormShow();
        }

        public List<string> GetCategories()
        {
            return _mainFormService.GetCategories();
        }
        public ThemeSizeCount GetSizeByTheme(string theme)
        {
            return _mainFormService.GetSizeByTheme(theme);
        }
    }
    public class ThemeSizeCount
    {
        public string Theme;
        public int Size;
        public ThemeSizeCount(string theme, int size)
        {
            Theme = theme;
            Size = size;
        }
    }
    
}
